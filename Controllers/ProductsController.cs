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
        private readonly IServiceManager _service;

        public ProductsController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("api/products")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _service.ProductService.GetAllProductsAsync(trackChanges: false);

            return Ok(products);
        }

        [HttpGet]
        [Route("api/products/{productId:int}")]
        public async Task<IActionResult> GetProduct(int productId)
        {
            var product = await _service.ProductService.GetProductByIdAsync(productId, trackChanges: false);

            return Ok(product);
        }

        [HttpGet]
        [Route("api/users/{userId:int}/products")]
        public async Task<IActionResult> GetUserProduct(int userId)
        {
            var userProducts = await _service.ProductService.GetUserProductsAsync(userId, trackChanges: false);

            return Ok(userProducts);
        }
    }
}
