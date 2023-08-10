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
        public async Task<IActionResult> SignUp([FromBody] UserForSignUpDto userForSignUpDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await _services.AuthService.SignUp(userForSignUpDto);

            return Ok();
        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> SignIn([FromBody] UserForSignInDto userForSignInDto)
        {
            if (!await _services.AuthService.ValidateUser(userForSignInDto))
                return Unauthorized();

            return Ok(new { Token = await _services.AuthService.CreateToken(true) });
        }

        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> Logout()
        //{

        //}

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenDto tokenDto)
        {
            return Ok(await _services.AuthService.RefreshToken(tokenDto));
        }
    }
}
