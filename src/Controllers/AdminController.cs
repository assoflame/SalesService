using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

            return Ok(usersWithMetaData.users);
        }

        [HttpPatch("users/{userId:int}")]
        public async Task<IActionResult> BlockUser(int userId)
        {
            await _services.AdminService.BlockUser(userId);

            return NoContent();
        }

        [HttpDelete("products/{productId:int}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            await _services.ProductService.DeleteProductAsync(productId);

            return NoContent();
        }
    }
}
