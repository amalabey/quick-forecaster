using Microsoft.EntityFrameworkCore;
using QuickForecaster.Application.Clients.Commands;
using QuickForecaster.Application.UnitTests.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using QuickForecaster.Domain.Entities;
using FizzWare.NBuilder;
using QuickForecaster.Application.Exceptions;
using QuickForecaster.Application.Estimates.Commands;
using QuickForecaster.Application.Backlog.Commands;
using QuickForecaster.Application.Backlog.Queries;

namespace QuickForecaster.Application.UnitTests.Backlog
{
    public class GetBacklogItemsQueryTests
    {
        [Fact]
        public async Task Handle_WithValidEstimate_GetAllBacklogItems()
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
                .All().With(x => x.Id = 0)
                .TheFirst(10).With(e => e.Client = clients[0])
                .TheRest().With(e => e.Client = clients[1])
                .Build();

            var backlogItems = Builder<BacklogItem>
                .CreateListOfSize(30)
                .All().With(x => x.Id = 0)
                .TheFirst(10).With(bi => bi.Estimate = estimates[0])
                .TheNext(10).With(bi => bi.Estimate = estimates[1])
                .TheRest().With(bi => bi.Estimate = estimates[2])
                .Build();

            using (var db = new InMemoryDataContextBuilder()
                .WithDbName(dbName)
                .WithClients(clients)
                .WithEstimates(estimates)
                .WithBackloItems(backlogItems)
                .BuildScoped())
            {
                var handler = new GetBacklogItemsQueryHandler(db.Context);

                var result = await handler.Handle(new GetBacklogItemsQuery(1), CancellationToken.None);
                result.Should().NotBeNull();
                result.Count.Should().Be(10);
            }
        }
    }
}
