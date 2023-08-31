using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Services.Interfaces;
using Shared.DataTransferObjects;
using System.Security.Claims;
using System.Text.Json;

namespace Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IServiceManager _services;

        public UsersController(IServiceManager services)
        {
            _services = services;
        }

        [Authorize]
        [HttpPost]
        [Route("api/users/{sellerId:int}")]
        public async Task<IActionResult> RateUser(int sellerId, RateDto rateDto)
        {
            if (int.TryParse(HttpContext?.User.FindFirst("Id")?.Value, out var customerId) &&
                sellerId != customerId)
            {
                await _services.UserService.RateUser(customerId, sellerId, rateDto);
                return Ok();
            }

            return BadRequest();
        }

        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> SendMessage()
        //{

        //}
    }
}
