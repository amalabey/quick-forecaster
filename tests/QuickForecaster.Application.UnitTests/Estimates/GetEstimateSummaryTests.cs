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
using System;
using System.Collections.Generic;

namespace QuickForecaster.Application.UnitTests.Estimates
{
    public class GetEstimateSummaryTests
    {
        public static IEnumerable<object[]> CorrectEstimatedDates =>
            new List<object[]>
            {
                new object[] { 8, 16, 1, new DateTime(1900,1,11), new DateTime(1900,1,21)},
                new object[] { 16, 8, 1, new DateTime(1900,1,21), new DateTime(1900,1,11)}
            };

        [Theory]
        [MemberData(nameof(CorrectEstimatedDates))]
        public async Task Handle_WithExistingEstimates_ReturnCorrectSummary(decimal optimisticEstimate, 
            decimal pessimisticEstimate, 
            int teamSize, 
            DateTime optimisticEndDate, 
            DateTime pessimisticEndDate)
        {
            // Arrange
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
                // Setup mock
                var mockDateTimeProvider = Substitute.For<IDateTimeProvider>();
                mockDateTimeProvider.Now.Returns(new DateTime(1900, 1, 1));

                // Act
                var handler = new GetSummaryQueryHandler(db.Context, mockDateTimeProvider);
                var result = await handler.Handle(new GetSummaryQuery(estimates[0].Id, teamSize), CancellationToken.None);

                // Assert
                result.OptimisticEndDate.Should().BeSameDateAs(optimisticEndDate);
                result.PessimisticEndDate.Should().BeSameDateAs(pessimisticEndDate);
            }
        }
    }
}
