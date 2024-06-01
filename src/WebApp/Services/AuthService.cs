using AutoMapper;
using DataAccess.Interfaces;
using Entities.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SalesService.Entities.Models;
using Services.Interfaces;
using Shared;
using Shared.DataTransferObjects;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtSettings _jwtSettings;

        private User? _user;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper,
            IConfiguration configuration,
            IOptionsSnapshot<JwtSettings> jwtOptionsSnapshot)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _jwtSettings = jwtOptionsSnapshot.Value;
        }

        public async Task SignUpAsync(SignUpDto userSignUpDto)
        {
            var user = _mapper.Map<User>(userSignUpDto);

            user.PasswordSalt = ComputeSalt(user.Email);
            user.PasswordHash = ComputeMD5HashString(userSignUpDto.Password + user.PasswordSalt);

            _unitOfWork.Users.Create(user);

            var role = await _unitOfWork.Roles.GetByNameAsync("user");

            _unitOfWork.UserRoles.Create( new UserRole
                {
                    User = user,
                    RoleId = role!.Id
                }
            );

            await _unitOfWork.SaveAsync();
        }

        private static string ComputeSalt(string baseValue)
        {
            var bytes = Encoding.UTF8.GetBytes(baseValue);
            var hash = SHA256.HashData(bytes);

            return Convert.ToBase64String(hash);
        }

        public async Task<bool> ValidateUserAsync(SignInDto userForSignInDto)
        {
            _user = await _unitOfWork.Users
                .GetUserByEmailAsync(userForSignInDto.Email);

            if (_user == null || _user.Status == UserStatus.Blocked)
            {
                return false;
            }

            var passwordHash = ComputeMD5HashString(
                String.Concat(userForSignInDto.Password, _user.PasswordSalt));

            if (passwordHash != _user.PasswordHash)
            {
                return false;
            }

            return true;
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
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
                ValidateLifetime = true,
                ValidIssuer = _jwtSettings.ValidIssuer,
                ValidAudience = _jwtSettings.ValidAudience
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

        public async Task<(TokensInfo, AuthInfo)> CreateTokenAsync()
        {
            var signingCredentials = GetSigningCredentials();

            var claims = GetClaims();

            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            var refreshToken = GenerateRefreshToken();

            _user!.RefreshToken = refreshToken;
            _user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMonths(1);

            _unitOfWork.Users.Update(_user);

            await _unitOfWork.SaveAsync();

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return (
                new TokensInfo(accessToken, refreshToken),
                new AuthInfo(
                    int.Parse(claims.First(c => c.Type == "Id").Value),
                    claims.Where(c => c.Type == "role").Select(c => c.Value).ToArray(),
                    DateTime.UtcNow.AddMinutes(_jwtSettings.AccessExpiresInMunutes)
                ));
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private List<Claim> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim("Email", _user!.Email),
                new Claim("Status", _user.Status.ToString()),
                new Claim("Id", _user.Id.ToString())
            };

            var roles = _user.Roles.Select(ur => ur.Role);

            foreach (var role in roles)
            {
                claims.Add(new Claim("role", role.Name));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,
            List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken
            (
                issuer: _jwtSettings.ValidIssuer,
                audience: _jwtSettings.ValidAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessExpiresInMunutes),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }

        public async Task<(TokensInfo, AuthInfo)> RefreshTokenAsync(TokensInfo tokensInfo)
        {
            var principal = GetPrincipalFromExpiredToken(tokensInfo.AccessToken);

            var userEmail = principal!.FindFirst("Email")!.Value!;

            var user = await _unitOfWork.Users
                .GetUserByEmailAsync(userEmail);

            if (user is null || user.RefreshToken != tokensInfo.RefreshToken ||
                   user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                throw new RefreshTokenBadRequestException();
            }

            _user = user;
            return await CreateTokenAsync();
        }
    }
}
