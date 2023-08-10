using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    [ApiController]
    [Authorize(Roles = "admin")]
    public class AdminController : ControllerBase
    {
        private readonly IServiceManager _service;

        public AdminController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("api/users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _service.UserService.GetAllUsersAsync(trackChanges: false);

            return Ok(users);
        }

        [HttpPatch]
        [Route("api/admin/users/{userId:int}/block")]
        public async Task<IActionResult> BlockUser(int userId)
        {
            await _service.AdminService.BlockUser(userId);

            return Ok();
        }
    }
}
