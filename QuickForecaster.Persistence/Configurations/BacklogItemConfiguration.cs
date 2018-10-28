using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickForecaster.Domain.Entities;
using QuickForecaster.Domain.Enums;
using System;

namespace QuickForecaster.Persistence.Configurations
{
    public class BacklogItemConfiguration : IEntityTypeConfiguration<BacklogItem>
    {
        public void Configure(EntityTypeBuilder<BacklogItem> builder)
        {
            builder.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            builder
                .Property(e => e.Confidence)
                .HasConversion(
                    v => v.ToString(),
                    v => (ConfidenceLevel)Enum.Parse(typeof(ConfidenceLevel), v));

            builder.Property<int?>("EstimateId");

            builder.Property<decimal>("OptimisticEstimate");

            builder.Property<decimal>("PessimisticEstimate");

            builder.Property<string>("Task");

            builder.HasKey("Id");

            builder.HasIndex("EstimateId");

            builder.ToTable("BacklogItems");

            builder.HasOne(b => b.Estimate)
                        .WithMany(e => e.BacklogItems)
                        .HasForeignKey("EstimateId");
        }
    }
}
