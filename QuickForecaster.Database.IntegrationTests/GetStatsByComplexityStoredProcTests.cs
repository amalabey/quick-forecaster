using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using QuickForecaster.Domain.Entities;
using QuickForecaster.Persistence;
using StoredProcedureEFCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace QuickForecaster.Database.IntegrationTests
{
    public class GetStatsByComplexityStoredProcTests
    {
        private const string connectionString = @"Server=.\sqlexpress01;Database=QuickForecaster;Trusted_Connection=True;Application Name = QuickForecaster;";

        [Fact]
        public void SpGetStatsByComplexity_WithExistingBacklogItems_ReturnsAggregatedData()
        {
            var optionsBuilder = new DbContextOptionsBuilder<QuickForecasterDbContext>();
            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.UseSqlServer(connectionString);

            using (var context = new QuickForecasterDbContext(optionsBuilder.Options))
            {
                context.Database.EnsureCreated();

                List<Stat> stats = null;
                context.LoadStoredProc("[dbo].[spGetStatsByComplexity]")
                   .AddParam("EstimateId", 1)
                   .Exec(r => stats = r.ToList<Stat>());

                stats.Should().NotBeNull();
                stats.Count.Should().Be(3);
            }
        }
    }
}
