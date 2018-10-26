using Microsoft.EntityFrameworkCore;
using QuickForecaster.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickForecaster.Persistence
{
    public class QuickForecasterDbContext : DbContext
    {
        public QuickForecasterDbContext(DbContextOptions<QuickForecasterDbContext> options): base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Estimate> Estimates { get; set; }
        public DbSet<BacklogItem> BacklogItems { get; set; }
    }
}
