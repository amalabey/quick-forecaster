using Microsoft.EntityFrameworkCore;
using QuickForecaster.Domain.Entities;
using QuickForecaster.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickForecaster.Application.UnitTests.Infrastructure
{
    public class InMemoryDataContextBuilder
    {
        private string _dbName;
        private List<Client> _clients;

        public InMemoryDataContextBuilder WithDbName(string dbName)
        {
            _dbName = dbName;
            return this;
        }

        public InMemoryDataContextBuilder WithTestClients(int count, Func<TestClientDataBuilder, int, TestClientDataBuilder> builderFunc)
        {
            _clients = _clients ?? new List<Client>();
            for (int i = 0; i < count; i++)
            {
                var testClient = builderFunc(new TestClientDataBuilder(), i).Build();
                _clients.Add(testClient);
            }

            return this;
        }

        public ScopedInmemoryDatabase BuildScoped()
        {
            return new ScopedInmemoryDatabase(Build());
        }

        public QuickForecasterDbContext Build()
        {
            var options = new DbContextOptionsBuilder<QuickForecasterDbContext>()
                .UseInMemoryDatabase(this._dbName)
                .EnableSensitiveDataLogging(true)
                .Options;
            var context = new QuickForecasterDbContext(options);
            context.Database.EnsureCreated();

            if (_clients != null && _clients.Count > 0)
            {
                context.Clients.AddRange(_clients.ToArray());
                context.SaveChanges();
            }            

            return context;
        }
    }
}
