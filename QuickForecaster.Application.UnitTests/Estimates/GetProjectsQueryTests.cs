using FizzWare.NBuilder;
using FluentAssertions;
using QuickForecaster.Application.Clients.Queries;
using QuickForecaster.Application.Estimates.Queries;
using QuickForecaster.Application.UnitTests.Infrastructure;
using QuickForecaster.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace QuickForecaster.Application.UnitTests.Estimates
{
    public class GetProjectsQueryTests
    {
        [Fact]
        public async Task Handle_WithExistingEstimates_ReturnClientProjects()
        {
            var dbName = "QuickForecasterUnitTests";

            var clients = Builder<Client>
                .CreateListOfSize(10)
                .All()
                .With((c, index) => c.AccountManager = new Domain.ValueObjects.Contact($"Email-{index}", $"DisplayName-{index}"))
                .With(x => x.Id = 0)
                .Build();

            var estimates = Builder<Estimate>
                .CreateListOfSize(20)
                .TheFirst(10)
                .With(e => e.Client = clients[0])
                .TheRest()
                .With(e => e.Client = clients[1])
                .Build();

            string clientName = clients[0].Name;

            using (var db = new InMemoryDataContextBuilder()
                .WithDbName(dbName)
                .WithClients(clients)
                .WithEstimates(estimates)
                .BuildScoped())
            {
                var handler = new GetProjectsQueryHandler(db.Context);
                var clientEstimates = await handler.Handle(new GetProjectsQuery(db.Context.Clients.First(c => c.Name == clientName).Id), CancellationToken.None);

                clientEstimates.Should().NotBeNull();
                clientEstimates.Count.Should().Be(10);
            }
        }
    }
}
