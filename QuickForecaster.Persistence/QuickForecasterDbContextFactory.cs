using Microsoft.EntityFrameworkCore;
using QuickForecaster.Persistence.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickForecaster.Persistence
{
    public class QuickForecasterDbContextFactory : DesignTimeDbContextFactoryBase<QuickForecasterDbContext>
    {
        protected override QuickForecasterDbContext CreateNewInstance(DbContextOptions<QuickForecasterDbContext> options)
        {
            return new QuickForecasterDbContext(options);
        }
    }
}
