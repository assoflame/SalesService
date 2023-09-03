using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Shared.DataTransferObjects;
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
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IServiceManager _services;

        public ProductsController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductParameters productParameters)
        {
            var pagedProducts = await _services.ProductService
                .GetAllProductsAsync(productParameters);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(pagedProducts.metaData));

            return Ok(pagedProducts.products);
        }

        [HttpGet("{productId:int}")]
        public async Task<IActionResult> GetProduct(int productId)
        {
            var product = await _services.ProductService.GetProductByIdAsync(productId);

            return Ok(product);
        }

        [HttpGet]
        [Route("/api/users/{userId:int}/products")]
        public async Task<IActionResult> GetUserProducts(int userId, [FromQuery] ProductParameters productParameters)
        {
            var userProducts = await _services
                .ProductService
                .GetUserProductsAsync(userId, productParameters);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(userProducts.metaData));

            return Ok(userProducts.products);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductForCreationDto productForCreationDto)
        {
            if (productForCreationDto is null)
                return BadRequest("product for creationt dto object is null");

            if (int.TryParse(HttpContext?.User.FindFirst("Id")?.Value, out var userId))
            {
                var product = await _services.ProductService.CreateProductAsync(userId, productForCreationDto);

                return Ok();
            }

            return BadRequest();
        }

        [Authorize]
        [HttpPost("{productId:int}")]
        public async Task<IActionResult> UploadPhotos(int productId, IFormFileCollection files)
        {
            if (int.TryParse(HttpContext?.User.FindFirst("Id")?.Value, out var userId))
            {
                await _services.ProductService.AddPhotosAsync(userId, productId, files);
                return Ok();
            }

            return BadRequest();
        }
    }
}
