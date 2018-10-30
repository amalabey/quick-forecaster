using QuickForecaster.Application.UnitTests.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using QuickForecaster.Domain.Entities;
using FizzWare.NBuilder;
using QuickForecaster.Application.Backlog.Queries;
using QuickForecaster.Application.Estimates.Queries;
using NSubstitute;
using QuickForecaster.Common;

namespace QuickForecaster.Application.UnitTests.Estimates
{
    public class GetEstimateSummaryTests
    {
        [Theory]
        [InlineData(5, 10, 1)]
        [InlineData(1, 1000, 1)]
        [InlineData(100, 1000, 5)]
        public async Task Handle_WithExistingEstimates_ReturnCorrectSummary(decimal optimisticEstimate, decimal pessimisticEstimate, int teamSize)
        {
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
                    .With(bi => bi.OptimisticEstimate = optimisticEstimate)
                    .With(bi => bi.PessimisticEstimate = pessimisticEstimate)
                .TheNext(10).With(bi => bi.Estimate = estimates[1])
                .TheRest().With(bi => bi.Estimate = estimates[2])
                .Build();

            using (var db = new InMemoryDataContextBuilder()
                .WithClients(clients)
                .WithEstimates(estimates)
                .WithBackloItems(backlogItems)
                .BuildScoped())
            {
                var startDate = new System.DateTime(2000, 1, 1);

                // Setup mock
                var mockDateTimeProvider = Substitute.For<IDateTimeProvider>();
                mockDateTimeProvider.Now.Returns(startDate);

                var handler = new GetSummaryQueryHandler(db.Context, mockDateTimeProvider);

                var result = await handler.Handle(new GetSummaryQuery(estimates[0].Id, teamSize), CancellationToken.None);

                decimal expectedOptimisticDuration = ((optimisticEstimate * 10) / 8) / teamSize;
                decimal expectedPessimisticDuration = ((pessimisticEstimate * 10) / 8) / teamSize;

                result.OptimisticDuration.Should().Be(expectedOptimisticDuration);
                result.PessimisticDuration.Should().Be(expectedPessimisticDuration);
                result.OptimisticEndDate.Should().BeSameDateAs(startDate.AddDays((double)expectedOptimisticDuration));
                result.PessimisticEndDate.Should().BeSameDateAs(startDate.AddDays((double)expectedPessimisticDuration));
            }
        }
    }
}
