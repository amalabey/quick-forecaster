using FizzWare.NBuilder;
using FluentAssertions;
using QuickForecaster.Application.Clients.Queries;
using QuickForecaster.Application.UnitTests.Infrastructure;
using QuickForecaster.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace QuickForecaster.Application.UnitTests.Clients
{
    [Collection("ClientTests")]
    [Trait("Category", "ClientTests")]
    public class GetClientsQueryTests
    {
        [Fact]
        public async Task Handle_WithExistingClients_ReturnAllClients()
        {
            var clients = Builder<Client>
                .CreateListOfSize(10)
                .All()
                .With((c, index) => c.AccountManager = new Domain.ValueObjects.Contact($"Email-{index}", $"DisplayName-{index}"))
                .With(x => x.Id = 0)
                .Build();

            using (var db = new InMemoryDataContextBuilder()
                .WithClients(clients)
                .BuildScoped())
            {
                var handler = new GetAllClientsQueryHandler(db.Context);
                var allClients = await handler.Handle(new GetAllClientsQuery(), CancellationToken.None);

                allClients.Should().NotBeNull();
                allClients.Count.Should().Be(10);
            }
        }
    }
}
