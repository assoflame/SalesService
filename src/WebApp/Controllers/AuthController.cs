using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Shared.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Shared;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager _services;
        private readonly JwtSettings _jwtSettings;

        public AuthController(IServiceManager services, IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _services = services;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto userSignUpDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await _services.AuthService.SignUpAsync(userSignUpDto);

            return Ok();
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInDto userSignInDto)
        {
            if (!await _services.AuthService.ValidateUserAsync(userSignInDto))
                return Unauthorized();

            var (tokensInfo, authInfo) = await _services.AuthService.CreateTokenAsync(populateExp: true);

            AddTokenToCookies(tokensInfo);

            return Ok(authInfo);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            var oldTokensInfo = new TokensInfo(
                    HttpContext.Request.Cookies["access-token"],
                    HttpContext.Request.Cookies["refresh-token"]
                );

            var (tokensInfo, authInfo) = await _services.AuthService.RefreshTokenAsync(oldTokensInfo);

            //var serializedTokenDto = JsonSerializer.Serialize(tokenDto);

            AddTokenToCookies(tokensInfo);

            return Ok(authInfo);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            AddTokenToCookies(new TokensInfo(string.Empty, string.Empty));

            return Ok();
        }

        private void AddTokenToCookies(TokensInfo tokens)
        {
            HttpContext.Response.Cookies.Append("access-token", tokens.AccessToken,
                new()
                {
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None
                });

            HttpContext.Response.Cookies.Append("refresh-token", tokens.RefreshToken,
                new()
                {
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None
                });
        }
    }
}
