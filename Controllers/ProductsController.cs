using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IServiceManager _services;

        public ProductsController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        [Route("api/products")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _services.ProductService.GetAllProductsAsync();

            return Ok(products);
        }

        [HttpGet]
        [Route("api/products/{productId:int}")]
        public async Task<IActionResult> GetProduct(int productId)
        {
            var product = await _services.ProductService.GetProductByIdAsync(productId);

            return Ok(product);
        }

        [HttpGet]
        [Route("api/users/{userId:int}/products")]
        public async Task<IActionResult> GetUserProducts(int userId)
        {
            var userProducts = await _services.ProductService.GetUserProductsAsync(userId);

            return Ok(userProducts);
        }
    }
}
