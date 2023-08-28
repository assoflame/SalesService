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
        private readonly IServiceManager _services;

        public AdminController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        [Route("api/admin/users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _services.UserService.GetAllUsersAsync();

            return Ok(users);
        }

        [HttpPatch]
        [Route("api/admin/users/{userId:int}")]
        public async Task<IActionResult> BlockUser(int userId)
        {
            await _services.AdminService.BlockUser(userId);

            return NoContent();
        }

        [HttpDelete]
        [Route("api/admin/products/{productId:int}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            await _services.ProductService.DeleteProductAsync(productId);

            return NoContent();
        }
    }
}
