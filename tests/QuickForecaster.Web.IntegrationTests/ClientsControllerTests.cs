using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        [Fact]
        [AutoRollback]
        public async Task Create_WithValidClient_AndThenReturnAll_IncludesNewClient()
        {
            var client = _factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false,
                    BaseAddress = new Uri("http://localhost")
                });

            // Create client
            var requestContent = new StringContent(@"{
                                    'name': 'Microsoft',
                                    'accountManagerEmail': 'ms@ms.com',
                                    'accountManagerName' : 'acc mgr ms'
                                }");
            requestContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            await client.PostAsync("http://localhost/api/clients", requestContent);

            // Get all clients
            var response = await client.GetAsync("http://localhost/api/clients");
            var content = await response.Content.ReadAsStringAsync();
            JArray clientsJson = JArray.Parse(content);

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.NotNull(content);
            Assert.Equal(5, clientsJson.Count);
        }
    }
}
