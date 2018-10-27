using Microsoft.EntityFrameworkCore;
using QuickForecaster.Application.Clients.Commands;
using QuickForecaster.Application.UnitTests.Infrastructure;
using QuickForecaster.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace QuickForecaster.Application.UnitTests.Clients
{
    public class CreateClientCommandTests
    {
        [Fact]
        public async Task Handle_WithValidClientDetails_CreateTheRecord()
        {
            var dbName = "QuickForecasterUnitTests";

            using (var db = new InMemoryDataContextBuilder()
                .WithDbName(dbName)
                .WithTestClients(10, (builder, index) => builder.WithDummyClient(index).WithDummyAccountManager(index))
                .BuildScoped())
            {
                var handler = new CreateClientCommandHandler(db.Context);
                await handler.Handle(new CreateClientCommand
                {
                    Name = "Fabrikam",
                    AccountManagerEmail = "mgr1@fabrikam.com",
                    AccountManagerName = "Manager One"
                }, CancellationToken.None);

                using (var varifyContext = new InMemoryDataContextBuilder().WithDbName(dbName).Build())
                {
                    varifyContext.Clients
                        .FirstOrDefaultAsync(c => c.Name == "Fabrikam")
                        .Should()
                        .NotBeNull();
                }
            }
        }
    }
}
