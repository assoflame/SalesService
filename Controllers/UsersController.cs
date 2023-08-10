using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Services.Interfaces;
using Shared.DataTransferObjects;
using System.Text.Json;

namespace Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IServiceManager _service;

        public UsersController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        [Route("api/users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _service.UserService.GetUsersAsync();

            return Ok(users);
        }

        [HttpGet]
        [Route("api/users/{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _service.UserService.GetUserAsync(id);

            return Ok(user);
        }

        //[HttpGet]
        //[Route("api/users/{userId:int}/products/{productId:int}")]
        //public async Task<IActionResult> GetUserProducts(int userId, int productId)
        //{
        //    var result = new { userId = userId, productId = productId };

        //    return Ok(JsonSerializer.Serialize(result));
        //}

    }
}
