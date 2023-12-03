using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class ProductsTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public ProductsTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        private async Task<FactoryClients> GetFactoryClients()
        {
            var clients = new FactoryClients()
            {
                UnauthorizedClient = _factory.CreateClient(),
                AuthorizedClient = _factory.CreateClient(),
                AdminClient = _factory.CreateClient()
            };

            await InitAuthorizationHeader(
                clients.AuthorizedClient, new SignInDto() { Email = "test@gmail.com", Password = "test" });

            await InitAuthorizationHeader(
                clients.AdminClient, new SignInDto() { Email = "admin@gmail.com", Password = "SUPERUSER" });

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
        public async Task GetProduct_NotFound()
        {
            // Arrange

            var incorrectProductId = -1;
            var unauthorizedClient = (await GetFactoryClients()).UnauthorizedClient;

            // Act

            var response = await unauthorizedClient
                .GetAsync($"api/products/{incorrectProductId}");

            // Assert

            Assert.NotNull(response);
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateValidProduct()
        {
            // Arrange
            var authorizedClient = (await GetFactoryClients()).AdminClient;
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
            var authorizedClient = (await GetFactoryClients()).AdminClient;
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
    }
}
