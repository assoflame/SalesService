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
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IServiceManager _services;

        public UsersController(IServiceManager services)
        {
            _services = services;
        }

        [Authorize]
        [HttpPost("{sellerId:int}")]
        public async Task<IActionResult> RateUser(int sellerId, [FromBody] RateDto rateDto)
        {
            if (rateDto is null)
                return BadRequest();

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
