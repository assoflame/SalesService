using AutoMapper;
using Controllers;
using DataAccess.Interfaces;
using Entities.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using SalesService.Entities.Models;
using Services.Interfaces;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Web;
using Xunit;

namespace Tests.Controllers
{
    public class ProductsControllerTests
    {
        private List<Product> products;
        private IMapper mapper;
        private ProductParameters productParams;

        public ProductsControllerTests()
        {
            products = new List<Product>()
            {
                new Product
                {
                    Id = 1,
                    UserId = 1,
                    Name = "First Product",
                    Description = "First Product Description",
                    Price = 1000,
                    IsSold = false,
                    CreationDate = DateTime.UtcNow
                },
                new Product
                {
                    Id = 2,
                    UserId = 1,
                    Name = "second Product",
                    Description = "second Product Description",
                    Price = 2000,
                    IsSold = false,
                    CreationDate = DateTime.UtcNow
                },new Product
                {
                    Id = 3,
                    UserId = 2,
                    Name = "Third Product",
                    Description = "Third Product Description",
                    Price = 3000,
                    IsSold = false,
                    CreationDate = DateTime.UtcNow
                }
            };

            productParams = new ProductParameters();

            mapper = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()))
                .CreateMapper();
        }

        [Fact]
        public async Task GetProducts_Ok()
        {
            // Arrange

            var productService = new Mock<IProductService>();
            var serviceManager = new Mock<IServiceManager>();
            serviceManager.Setup(sm => sm.ProductService).Returns(productService.Object);

            var pagedProducts = PagedList<Product>
                .ToPagedList(products, productParams.PageNumber, productParams.PageSize);

            productService.Setup(ps => ps.GetAllProductsAsync(productParams).Result)
                .Returns((mapper.Map<IEnumerable<ProductDto>>(pagedProducts), pagedProducts.MetaData));
            var productsController = new ProductsController(serviceManager.Object);
            productsController.ControllerContext.HttpContext = new DefaultHttpContext();

            // Act

            var result = await productsController.GetProducts(productParams) as OkObjectResult;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.NotNull(result.Value);
            var returnModel = Assert.IsAssignableFrom<List<ProductDto>>(result.Value);
            Assert.Equal(pagedProducts.Count, returnModel.Count);

            Assert.Equal(JsonSerializer.Serialize(pagedProducts.MetaData),
                productsController.Response.Headers["X-Pagination"]);
        }

        [Fact]
        public async Task GetProduct_Ok()
        {
            // Arrange

            int productId = 1;
            var productService = new Mock<IProductService>();
            var serviceManager = new Mock<IServiceManager>();
            serviceManager.Setup(sm => sm.ProductService).Returns(productService.Object);
            productService.Setup(ps => ps.GetProductByIdAsync(productId).Result)
                .Returns(mapper.Map<ProductDto>(products.Where(p => p.Id == productId)
                    .FirstOrDefault()));
            var productsController = new ProductsController(serviceManager.Object);
            productsController.ControllerContext.HttpContext = new DefaultHttpContext();

            // Act

            var result = await productsController.GetProduct(productId) as OkObjectResult;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.NotNull(result.Value);
            var returnModel = Assert.IsAssignableFrom<ProductDto>(result.Value);
            Assert.Equal(productId, returnModel.Id);
        }

        [Fact]
        public async Task GetUserProducts_Ok()
        {
            // Arrange

            int userId = 1;
            var productService = new Mock<IProductService>();
            var serviceManager = new Mock<IServiceManager>();
            serviceManager.Setup(sm => sm.ProductService).Returns(productService.Object);

            var pagedProducts = PagedList<Product>
                .ToPagedList(products.Where(p => p.UserId == userId),
                    productParams.PageNumber, productParams.PageSize);

            productService.Setup(ps => ps.GetUserProductsAsync(userId, productParams).Result)
                .Returns((mapper.Map<IEnumerable<ProductDto>>(pagedProducts), pagedProducts.MetaData));

            var productsController = new ProductsController(serviceManager.Object);
            productsController.ControllerContext.HttpContext = new DefaultHttpContext();

            // Act

            var result = await productsController
                .GetUserProducts(userId, productParams) as OkObjectResult;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.NotNull(result.Value);
            var returnModel = Assert.IsAssignableFrom<List<ProductDto>>(result.Value);
            Assert.Equal(pagedProducts.Count, returnModel.Count);
            Assert.Equal(JsonSerializer.Serialize(pagedProducts.MetaData),
                productsController.Response.Headers["X-Pagination"]);
        }
    }

    public class IntegrationProductsTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public IntegrationProductsTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetProduct_NotFound()
        {
            // Arrange

            var client = _factory.CreateClient();
            var incorrectProductId = -1;

            // Act

            var response = await client.GetAsync($"api/products/{incorrectProductId}");

            // Assert

            Assert.NotNull(response);
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateValidProduct()
        {
            // Arrange

            var client = _factory.CreateClient();
            var signInResponse = await client.PostAsJsonAsync("api/auth/signin",
                new SignInDto { Email = "testuser@gmail.com", Password = "testuser" });
            var accessToken = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenDto>(
                await signInResponse.Content.ReadAsStringAsync()).AccessToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string> ("Name", "New Product"),
                new KeyValuePair<string, string> ("Description", "Description"),
                new KeyValuePair<string, string> ("Price", "10000")
            });

            // Act

            var response = await client.PostAsync("api/products", formData);
            var content = Newtonsoft.Json.JsonConvert
                .DeserializeObject<ProductDto>(await response.Content.ReadAsStringAsync());

            // Assert

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(content);
            Assert.False(content.IsSold);
        }

        [Fact]
        public async Task CreateInvalidProduct()
        {
            // Arrange

            var client = _factory.CreateClient();
            var signInResponse = await client.PostAsJsonAsync("api/auth/signin",
                new SignInDto { Email = "testuser@gmail.com", Password = "testuser" });
            var accessToken = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenDto>(
                await signInResponse.Content.ReadAsStringAsync()).AccessToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string> ("Name", "New Product"),
                new KeyValuePair<string, string> ("Description", "Description"),
                new KeyValuePair<string, string> ("Price", "-10000")
            });

            // Act

            var response = await client.PostAsync("api/products", formData);

            // Assert

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        //[Fact]
        //public async Task DeleteOwnExistingProduct()
        //{
        //    // Arrange

        //    int productId;
        //    var client = _factory.CreateClient();
        //    var signInResponse = await client.PostAsJsonAsync("api/auth/signin",
        //        new SignInDto { Email = "testuser@gmail.com", Password = "testuser" });
        //    var accessToken = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenDto>(
        //        await signInResponse.Content.ReadAsStringAsync()).AccessToken;
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        //    // Act

        //    var response = await client.DeleteAsync("api/products/")
        //}
    }
}