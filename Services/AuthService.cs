﻿using AutoMapper;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SalesService.Entities.Models;
using Services.Interfaces;
using Shared.DataTransferObjects;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        private User _user;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, ILoggerManager logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task SignUp(UserForSignUpDto userForSignUpDto)
        {
            var user = _mapper.Map<User>(userForSignUpDto);

            user.PasswordSalt = user.FirstName + user.LastName;
            user.PasswordHash = ComputeMD5HashString(userForSignUpDto.Password + user.PasswordSalt);

            await _unitOfWork.Users.CreateAsync(user);

            var role = await _unitOfWork.Roles.GetByNameAsync("user");

            await _unitOfWork.UserRoles.CreateAsync(
                new UserRole { User = user, Role = role }
                );

            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> ValidateUser(UserForSignInDto userForSignInDto)
        {
            _user = await _unitOfWork.Users
                .GetAsync(u => u.Email == userForSignInDto.Email);

            if (_user == null)
            {
                _logger.LogError($"{nameof(ValidateUser)}: Authentication failed. Wrong email");
                return false;
            }

            var passwordHash = ComputeMD5HashString(
                String.Concat(userForSignInDto.Password, _user.FirstName, _user.LastName));

            if (passwordHash != _user.PasswordHash)
            {
                _logger.LogError($"{nameof(ValidateUser)}: Authentication failed. Wrong password");
                return false;
            }
            return true;
        }

        public Task LogOut()
        {
            // delete tokens
            throw new NotImplementedException();
        }

        private string ComputeMD5HashString(string str)
        {
            var md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            return Convert.ToHexString(hash);
        }

        private string GenerateRefreshToken()
        {
            var rndNumber = new byte[32];
            var rndGenerator = RandomNumberGenerator.Create();
            rndGenerator.GetBytes(rndNumber);

            return Convert.ToBase64String(rndNumber);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings["secretKey"])),
                ValidateLifetime = true,
                ValidIssuer = jwtSettings["validIssuer"],
                ValidAudience = jwtSettings["validAudience"]
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out
                securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                                           StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        public async Task<TokenDto> CreateToken(bool populateExp)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var refreshToken = GenerateRefreshToken();
            _user.RefreshToken = refreshToken;
            if (populateExp)
                _user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            _unitOfWork.Users.Update(_user);
            await _unitOfWork.SaveAsync();
            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return new TokenDto(accessToken, refreshToken);
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings")["secretKey"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.Email)
            };

            var roles = (await _unitOfWork.Users.GetAsync(
                    include: u => u.Include(us => us.Roles)
                .ThenInclude(ur => ur.Role))).Roles
                .Select(role => role.Role);

            foreach (var role in roles)
            {
                claims.Add(new Claim("role", role.Name));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,
            List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Double.Parse(jwtSettings["expires"])),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }

        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);

            var user = await _unitOfWork.Users.GetAsync(user => user.Email == principal.Identity.Name);
            if (user is null || user.RefreshToken != tokenDto.RefreshToken ||
                   user.RefreshTokenExpiryTime > DateTime.Now)
            {
                //throw new RefreshTokenBadRequest();
                throw new ArgumentException();
            }

            _user = user;
            return await CreateToken(populateExp: false);
        }
    }
}
