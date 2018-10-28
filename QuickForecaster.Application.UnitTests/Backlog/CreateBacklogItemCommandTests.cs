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

namespace QuickForecaster.Application.UnitTests.Backlog
{
    public class CreateBacklogItemCommandTests
    {
        [Fact]
        public async Task Handle_WithValidBacklogItem_CreateTheRecord()
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

            using (var db = new InMemoryDataContextBuilder()
                .WithDbName(dbName)
                .WithClients(clients)
                .WithEstimates(estimates)
                .BuildScoped())
            {
                var handler = new CreateBacklogItemCommandHandler(db.Context);
                string task = "Create home page";
                await handler.Handle(new CreateBackloItemCommand
                {
                    EstimateId = 1,
                    Task = task,
                    Confidence = Domain.Enums.ConfidenceLevel.High,
                    OptimisticEstimate = 6,
                    PessimisticEstimate = 8
                }, CancellationToken.None);

                using (var varifyContext = new InMemoryDataContextBuilder().WithDbName(dbName).Build())
                {
                    varifyContext.BacklogItems
                        .Include(bi => bi.Estimate)
                        .FirstOrDefaultAsync(bi => bi.Estimate.Id == 1 && bi.Task == task)
                        .Should()
                        .NotBeNull();
                }
            }
        }
    }
}
