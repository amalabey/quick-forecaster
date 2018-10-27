using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using QuickForecaster.Application.Clients.Queries;
using QuickForecaster.Application.UnitTests.Infrastructure;
using QuickForecaster.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace QuickForecaster.Application.UnitTests.Clients
{
    public class GetClientsQueryTests
    {
        [Fact]
        public async Task Handle_WithExistingClients_ReturnAllClients()
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
                var handler = new GetAllClientsQueryHandler(db.Context);
                var allClients = await handler.Handle(new GetAllClientsQuery(), CancellationToken.None);

                allClients.Should().NotBeNull();
                allClients.Count.Should().Be(10);
            }
        }
    }
}
