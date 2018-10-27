using Microsoft.EntityFrameworkCore;
using QuickForecaster.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickForecaster.Application.UnitTests.Infrastructure
{
    public static class QuickForecasterContextFactory
    {
        public static QuickForecasterDbContext Create(string dbName)
        {
            var options = new DbContextOptionsBuilder<QuickForecasterDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            var context = new QuickForecasterDbContext(options);

            context.Database.EnsureCreated();
            context.SaveChanges();

            return context;
        }

        public static QuickForecasterDbContext Create()
        {
            return Create(Guid.NewGuid().ToString());
        }

        public static void Destroy(QuickForecasterDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
