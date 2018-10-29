using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace QuickForecaster.Web.IntegrationTests
{
    public class ClientsControllerTests : IClassFixture<WebApplicationFactory<QuickForecaster.Web.Startup>>
    {
        private readonly WebApplicationFactory<QuickForecaster.Web.Startup> _factory;

        public ClientsControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllClients_WithExistingClients_ReturnsAll()
        {
            var client = _factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false,
                    BaseAddress = new Uri("http://localhost")
                });

            var response = await client.GetAsync("http://localhost/api/clients");
            var content = await response.Content.ReadAsStringAsync();
            JArray clientsJson = JArray.Parse(content);

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.NotNull(content);
            Assert.Equal(4, clientsJson.Count);
        }
    }
}
