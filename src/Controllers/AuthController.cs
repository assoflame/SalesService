using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Shared.DataTransferObjects;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager _services;

        public AuthController(IServiceManager services)
        {
            _services = services;
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto userSignUpDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await _services.AuthService.SignUpAsync(userSignUpDto);

            return Ok();
        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInDto userSignInDto)
        {
            if (!await _services.AuthService.ValidateUserAsync(userSignInDto))
                return Unauthorized();

            return Ok(new { Token = await _services.AuthService.CreateTokenAsync(true) });
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenDto tokenDto)
        {
            return Ok(await _services.AuthService.RefreshTokenAsync(tokenDto));
        }
    }
}
