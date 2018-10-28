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
using QuickForecaster.Domain.Enums;

namespace QuickForecaster.Application.UnitTests.Backlog
{
    public class UpdateBacklogItemCommandTests
    {
        [Fact]
        public async Task Handle_WithValidBackloItemDetails_UpdatesTheRecord()
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
                var handler = new UpdateBacklogItemCommandHandler(db.Context);
                string task = "Create contact-us page";
                await handler.Handle(new UpdateBackloItemCommand
                {
                    BacklogItemId = 1,
                    Task = task,
                    Confidence = Domain.Enums.ConfidenceLevel.Low,
                    OptimisticEstimate = 10,
                    PessimisticEstimate = 20
                }, CancellationToken.None);

                using (var varifyContext = new InMemoryDataContextBuilder().WithDbName(dbName).Build())
                {
                    var updatedItem = await varifyContext.BacklogItems.FirstOrDefaultAsync(bi => bi.Id == 1);

                    updatedItem.Should().NotBeNull();
                    updatedItem.Task.Should().Be(task);
                    updatedItem.Confidence.Should().Be(ConfidenceLevel.Low);
                    updatedItem.OptimisticEstimate.Should().Be(10);
                    updatedItem.PessimisticEstimate.Should().Be(20);
                }
            }
        }
    }
}
