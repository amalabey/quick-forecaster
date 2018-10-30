using Microsoft.EntityFrameworkCore;
using QuickForecaster.Application.Clients.Commands;
using QuickForecaster.Application.UnitTests.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using QuickForecaster.Domain.Entities;
using FizzWare.NBuilder;
using System.Linq;
using System;
using QuickForecaster.Application.Exceptions;
using QuickForecaster.Domain.ValueObjects;

namespace QuickForecaster.Application.UnitTests.Clients
{
    [Collection("ClientTests")]
    [Trait("Category", "ClientTests")]
    public class CreateClientCommandTests
    {
        [Fact]
        public async Task Handle_WithValidClientDetailsWithoutAccountMgr_CreatesTheRecord()
        {
            using (var db = new InMemoryDataContextBuilder().BuildScoped())
            {
                var handler = new CreateClientCommandHandler(db.Context);
                await handler.Handle(new CreateClientCommand
                {
                    Name = "Fabrikam"
                }, CancellationToken.None);

                using (var varifyContext = new InMemoryDataContextBuilder().WithDbName(db.DatabaseName).Build())
                {
                    var client = await varifyContext.Clients.FirstOrDefaultAsync(c => c.Name == "Fabrikam");
                    client.Should().NotBeNull();
                }
            }
        }

        [Fact]
        public async Task Handle_WithValidClientDetailsWithAccountMgr_CreatesTheRecord()
        {
            using (var db = new InMemoryDataContextBuilder().BuildScoped())
            {
                var handler = new CreateClientCommandHandler(db.Context);
                await handler.Handle(new CreateClientCommand
                {
                    Name = "Contoso",
                    AccountManagerEmail = "mgr1@fabrikam.com",
                    AccountManagerName = "Manager One"
                }, CancellationToken.None);

                using (var varifyContext = new InMemoryDataContextBuilder().WithDbName(db.DatabaseName).Build())
                {
                    var client = await varifyContext.Clients.FirstOrDefaultAsync(c => c.Name == "Contoso");
                    client.AccountManager.Should()
                        .Match<Contact>(am => am.DisplayName == "Manager One" && am.Email == "mgr1@fabrikam.com");
                }
            }
        }

        [Fact]
        public async Task Handle_WithEmptyClientName_ThrowsException()
        {
            using (var db = new InMemoryDataContextBuilder()
                .BuildScoped())
            {
                var handler = new CreateClientCommandHandler(db.Context);

                await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new CreateClientCommand
                {
                    Name = string.Empty,
                    AccountManagerEmail = "mgr1@fabrikam.com",
                    AccountManagerName = "Manager One"
                }, CancellationToken.None));
            }
        }
    }
}
