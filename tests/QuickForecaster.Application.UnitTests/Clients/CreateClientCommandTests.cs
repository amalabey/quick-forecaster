﻿using Microsoft.EntityFrameworkCore;
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

namespace QuickForecaster.Application.UnitTests.Clients
{
    [Collection("ClientTests")]
    [Trait("Category", "ClientTests")]
    public class CreateClientCommandTests
    {
        [Fact]
        public async Task Handle_WithValidClientDetails_CreateTheRecord()
        {
            using (var db = new InMemoryDataContextBuilder().BuildScoped())
            {
                var handler = new CreateClientCommandHandler(db.Context);
                await handler.Handle(new CreateClientCommand
                {
                    Name = "Fabrikam",
                    AccountManagerEmail = "mgr1@fabrikam.com",
                    AccountManagerName = "Manager One"
                }, CancellationToken.None);

                using (var varifyContext = new InMemoryDataContextBuilder().WithDbName(db.DatabaseName).Build())
                {
                    varifyContext.Clients
                        .FirstOrDefaultAsync(c => c.Name == "Fabrikam")
                        .Should()
                        .NotBeNull();
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
