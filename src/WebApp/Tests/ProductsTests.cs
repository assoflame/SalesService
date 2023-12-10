using Shared.DataTransferObjects;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Tests
{
    public class ProductsTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public ProductsTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        private async Task<Clients> GetCLients()
        {
            var clients = new Clients()
            {
                UnauthorizedClient = _factory.CreateClient(),
                AuthorizedClient = _factory.CreateClient(),
                AdminClient = _factory.CreateClient()
            };

            await InitAuthorizationHeader(
                clients.AuthorizedClient, new SignInDto() { Email = "test@gmail.com", Password = "Test123$" });

            await InitAuthorizationHeader(
                clients.AdminClient, new SignInDto() { Email = "admin@gmail.com", Password = "Admin123&" });

            return clients;
        }

        private static async Task InitAuthorizationHeader(HttpClient httpClient, SignInDto signInDto)
        {
            var signInResponse = await httpClient
                .PostAsJsonAsync("api/auth/signin", signInDto);

            var accessToken = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenDto>(
                await signInResponse.Content.ReadAsStringAsync()).AccessToken;

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        [Fact]
        public async Task GetProducts()
        {
            // Arrange
            var unauthorizedClient = (await GetCLients()).UnauthorizedClient;

            // Act

            var response = await unauthorizedClient
                .GetAsync("api/products");

            // Assert

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = Newtonsoft.Json.JsonConvert
                .DeserializeObject<List<ProductDto>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(content);
            Assert.True(response.Headers.Contains("X-Pagination"));
        }

        [Fact]
        public async Task GetProduct_NotFound()
        {
            // Arrange

            var incorrectProductId = -1;
            var unauthorizedClient = (await GetCLients()).UnauthorizedClient;

            // Act

            var response = await unauthorizedClient
                .GetAsync($"api/products/{incorrectProductId}");

            // Assert

            Assert.NotNull(response);
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


        [Fact]
        public async Task GetExistingProduct()
        {
            // Arrange
            var unauthorizedClient = (await GetCLients()).UnauthorizedClient;
            int productId = 1;

            // Act
            var response = await unauthorizedClient
                .GetAsync($"api/products/{productId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = Newtonsoft.Json.JsonConvert
                .DeserializeObject<ProductDto>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(content);
            Assert.Equal(productId, content.Id);
        }

        [Fact]
        public async Task CreateValidProduct()
        {
            // Arrange
            var authorizedClient = (await GetCLients()).AuthorizedClient;
            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string> ("Name", "New Product"),
                new KeyValuePair<string, string> ("Description", "Description"),
                new KeyValuePair<string, string> ("Price", "10000")
            });

            // Act

            var response = await authorizedClient.PostAsync("api/products", formData);
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
            var authorizedClient = (await GetCLients()).AuthorizedClient;
            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string> ("Name", "New Product"),
                new KeyValuePair<string, string> ("Description", "Description"),
                new KeyValuePair<string, string> ("Price", "-10000")
            });

            // Act

            var response = await authorizedClient.PostAsync("api/products", formData);

            // Assert

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateExistingProduct_ByOwner()
        {
            // Arrange
            var authorizedClient = (await GetCLients()).AuthorizedClient;
            int productId = 2;
            var productUpdateDto = new ProductUpdateDto
            {
                Name = "new name",
                Description = "new desctiption",
                Price = 999
            };

            // Act
            var response = await authorizedClient
                .PutAsJsonAsync($"api/products/{productId}", productUpdateDto);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        }

        [Fact]
        public async Task UpdateExistingProduct_ByOtherUser()
        {
            // Arrange
            var authorizedClient = (await GetCLients()).AuthorizedClient;
            int productId = 1;
            var productUpdateDto = new ProductUpdateDto
            {
                Name = "new name",
                Description = "new description",
                Price = 999
            };

            // Act
            var response = await authorizedClient
                .PutAsJsonAsync($"api/products/{productId}", productUpdateDto);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdateNotExistingProduct()
        {
            // Arrange
            var authorizedClient = (await GetCLients()).AuthorizedClient;
            int productId = -1;
            var productUpdateDto = new ProductUpdateDto
            {
                Name = "new name",
                Description = "new description",
                Price = 999
            };

            // Act
            var response = await authorizedClient
                .PutAsJsonAsync($"api/products/{productId}", productUpdateDto);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
