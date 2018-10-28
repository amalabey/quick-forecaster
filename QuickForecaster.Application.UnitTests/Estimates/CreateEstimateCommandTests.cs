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

namespace QuickForecaster.Application.UnitTests.Estimates
{
    public class CreateEstimateCommandTests
    {
        [Fact]
        public async Task Handle_WithValidProjectDetails_CreateTheRecord()
        {
            var dbName = "QuickForecasterUnitTests";

            var clients = Builder<Client>
                .CreateListOfSize(10)
                .All()
                .With((c, index) => c.AccountManager = new Domain.ValueObjects.Contact($"Email-{index}", $"DisplayName-{index}"))
                .With(x => x.Id = 0)
                .Build();

            using (var db = new InMemoryDataContextBuilder()
                .WithDbName(dbName)
                .WithClients(clients)
                .BuildScoped())
            {
                var handler = new CreateEstimateCommandHandler(db.Context);
                await handler.Handle(new CreateEstimateCommand
                {
                    ClientId = 1,
                    Name = "ProjectX",
                    EstimatorEmail = "jdoe@contoso.com",
                    EstimatorName = "John Doe"
                }, CancellationToken.None);

                using (var varifyContext = new InMemoryDataContextBuilder().WithDbName(dbName).Build())
                {
                    varifyContext.Estimates
                        .FirstOrDefaultAsync(c => c.ProjectName == "ProjectX")
                        .Should()
                        .NotBeNull();
                }
            }
        }
    }
}
