using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickForecaster.Domain.Entities;

namespace QuickForecaster.Persistence.Configurations
{
    public class EstimateConfiguration : IEntityTypeConfiguration<Estimate>
    {
        public void Configure(EntityTypeBuilder<Estimate> builder)
        {
            builder.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            builder.Property<int?>("ClientId");

            builder.Property<string>("Estimator");

            builder.HasKey("Id");

            builder.HasIndex("ClientId");

            builder.ToTable("Estimates");

            builder.HasOne("QuickForecaster.Domain.Entities.Client")
                        .WithMany("Estimates")
                        .HasForeignKey("ClientId");
        }
    }
}
