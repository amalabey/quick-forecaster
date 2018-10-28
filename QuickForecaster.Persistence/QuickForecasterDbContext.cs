using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using QuickForecaster.Domain.Entities;
using QuickForecaster.Persistence.Extensions;
using QuickForecaster.Persistence.Seed;

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
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.ApplyAllConfigurations();

            /// Below was only required to get the ef migration generated
            ///modelBuilder.Seed();
        }
    }
}
