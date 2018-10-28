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
        private List<Estimate> _estimats;
        private List<BacklogItem> _backlogItems;

        public InMemoryDataContextBuilder WithDbName(string dbName)
        {
            _dbName = dbName;
            return this;
        }

        public InMemoryDataContextBuilder WithClients(IList<Client> clients)
        {
            _clients = _clients ?? new List<Client>();
            _clients.AddRange(clients);
            return this;
        }

        public InMemoryDataContextBuilder WithEstimates(IList<Estimate> estimates)
        {
            _estimats = _estimats ?? new List<Estimate>();
            _estimats.AddRange(estimates);
            return this;
        }

        public InMemoryDataContextBuilder WithBackloItems(IList<BacklogItem> backlogItems)
        {
            _backlogItems = _backlogItems ?? new List<BacklogItem>();
            _backlogItems.AddRange(backlogItems);
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
            
            if(_estimats != null && _estimats.Count > 0)
            {
                context.Estimates.AddRange(_estimats.ToArray());
                context.SaveChanges();
            }

            if(_backlogItems != null && _backlogItems.Count > 0)
            {
                context.BacklogItems.AddRange(_backlogItems.ToArray());
                context.SaveChanges();
            }

            return context;
        }
    }
}
