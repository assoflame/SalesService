using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using Shared.Validation;
using System.Text.Json;

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
        public async Task<IActionResult> GetProducts([FromQuery] ProductParameters productParams)
        {
            var pagedProducts = await _services.ProductService
                .GetAllProductsAsync(productParams);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(pagedProducts.metaData));

            Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");

            return Ok(pagedProducts.products);
        }

        [HttpGet("{productId:int}", Name = "ProductById")]
        public async Task<IActionResult> GetProduct(int productId)
        {
            var product = await _services.ProductService.GetProductByIdAsync(productId);

            return Ok(product);
        }

        [HttpGet("/api/users/{userId:int}/products")]
        public async Task<IActionResult> GetUserProducts(int userId, [FromQuery] ProductParameters productParams)
        {
            var userProducts = await _services
                .ProductService
                .GetUserProductsAsync(userId, productParams);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(userProducts.metaData));

            Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");

            return Ok(userProducts.products);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreationDto productCreationDto)
        {
            if (productCreationDto is null)
                return BadRequest("product for creation dto object is null");

            if (int.TryParse(HttpContext?.User.FindFirst("Id")?.Value, out var userId))
            {
                var product = await _services.ProductService.CreateProductAsync(userId, productCreationDto);

                return CreatedAtRoute("ProductById", new { productId = product.Id }, product);
            }

            return BadRequest();
        }

        [HttpGet("{productId:int}/photos", Name = "ProductPhotos")]
        public async Task<IActionResult> GetProductPhotos(int productId)
        {
            var photos = await _services.ProductService.GetProductPhotosAsync(productId);

            return Ok(photos);
        }

        [Authorize]
        [HttpPost("{productId:int}/photos")]
        public async Task<IActionResult> UploadPhotos(int productId, [ImageValidation] IFormFileCollection images)
        {
            if (images.Count == 0)
                return Ok();

            if (int.TryParse(HttpContext?.User.FindFirst("Id")?.Value, out var userId))
            {
                var photos = await _services.ProductService.AddPhotosAsync(userId, productId, images);

                return CreatedAtRoute("ProductPhotos", new { productId = productId }, photos);
            }

            return BadRequest();
        }

        [Authorize]
        [HttpDelete("{productId:int}/photos")]
        public async Task<IActionResult> DeleteProductPhotos(int productId)
        {
            if(int.TryParse(HttpContext?.User.FindFirst("Id")?.Value, out var userId))
            {
                await _services.ProductService.DeleteProductPhotosAsync(userId, productId);

                return Ok();
            }

            return BadRequest();
        }

        [Authorize]
        [HttpPut("{productId:int}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductUpdateDto productUpdateDto)
        {
            if (productUpdateDto is null)
                return BadRequest("product update dto object is null");

            if (int.TryParse(HttpContext?.User.FindFirst("Id")?.Value, out var userId))
            {
                await _services.ProductService.UpdateProductAsync(userId, productId, productUpdateDto);

                return NoContent();
            }

            return BadRequest();
        }

        [Authorize]
        [HttpPatch("{productId:int}")]
        public async Task<IActionResult> SellProduct(int productId)
        {
            if (int.TryParse(HttpContext?.User.FindFirst("Id")?.Value, out var userId))
            {
                await _services.ProductService.SellProductAsync(userId, productId);

                return NoContent();
            }

            return BadRequest();
        }

        [Authorize]
        [HttpDelete("{productId:int}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            if (int.TryParse(HttpContext?.User.FindFirst("Id")?.Value, out var userId))
            {
                await _services.ProductService.DeleteProductAsync(productId, userId);

                return NoContent();
            }

            return BadRequest();
        }
    }
}
