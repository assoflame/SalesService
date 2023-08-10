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
        [Authorize(Roles = "admin")]
        [Route("api/users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _service.UserService.GetAllUsersAsync(trackChanges: false);

            return Ok(users);
        }

        [HttpGet]
        [Route("api/users/{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _service.UserService.GetUserByIdAsync(id, trackChanges: false);

            return Ok(user);
        }
    }
}
