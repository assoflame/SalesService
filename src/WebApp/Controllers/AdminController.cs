﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Shared.RequestFeatures;
using System.Text.Json;

namespace Controllers
{
    [ApiController]
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IServiceManager _services;

        public AdminController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers([FromQuery] UserParameters userParams)
        {
            var usersWithMetaData = await _services.UserService.GetAllUsersAsync(userParams);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(usersWithMetaData.metaData));

            Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");

            return Ok(usersWithMetaData.users);
        }

        [HttpGet("users/{userId:int}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _services.UserService.GetUserByIdAsync(userId);

            return Ok(user);
        }

        [HttpPatch("users/{userId:int}")]
        public async Task<IActionResult> BlockUser(int userId)
        {
            if (int.TryParse(HttpContext?.User?.FindFirst("Id")?.Value, out var adminId)
                && adminId != userId)
            {
                await _services.AdminService.BlockUser(userId);

                return NoContent();
            }

            return BadRequest();
        }
    }
}
