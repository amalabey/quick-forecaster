﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuickForecaster.Persistence;

namespace QuickForecaster.Persistence.Migrations
{
    [DbContext(typeof(QuickForecasterDbContext))]
    partial class QuickForecasterDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QuickForecaster.Domain.Entities.BacklogItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Confidence")
                        .IsRequired();

                    b.Property<int?>("EstimateId");

                    b.Property<decimal>("OptimisticEstimate");

                    b.Property<decimal>("PessimisticEstimate");

                    b.Property<string>("Task");

                    b.HasKey("Id");

                    b.HasIndex("EstimateId");

                    b.ToTable("BacklogItems");
                });

            modelBuilder.Entity("QuickForecaster.Domain.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("QuickForecaster.Domain.Entities.Estimate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClientId");

                    b.Property<int?>("ClientId1");

                    b.Property<string>("ProjectName");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("ClientId1");

                    b.ToTable("Estimates");
                });

            modelBuilder.Entity("QuickForecaster.Domain.Entities.BacklogItem", b =>
                {
                    b.HasOne("QuickForecaster.Domain.Entities.Estimate")
                        .WithMany("BacklogItems")
                        .HasForeignKey("EstimateId");
                });

            modelBuilder.Entity("QuickForecaster.Domain.Entities.Client", b =>
                {
                    b.OwnsOne("QuickForecaster.Domain.ValueObjects.Contact", "AccountManager", b1 =>
                        {
                            b1.Property<int>("ClientId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("DisplayName");

                            b1.Property<string>("Email");

                            b1.ToTable("Clients");

                            b1.HasOne("QuickForecaster.Domain.Entities.Client")
                                .WithOne("AccountManager")
                                .HasForeignKey("QuickForecaster.Domain.ValueObjects.Contact", "ClientId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("QuickForecaster.Domain.Entities.Estimate", b =>
                {
                    b.HasOne("QuickForecaster.Domain.Entities.Client")
                        .WithMany("Estimates")
                        .HasForeignKey("ClientId");

                    b.HasOne("QuickForecaster.Domain.Entities.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId1");

                    b.OwnsOne("QuickForecaster.Domain.ValueObjects.Contact", "Estimator", b1 =>
                        {
                            b1.Property<int>("EstimateId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("DisplayName");

                            b1.Property<string>("Email");

                            b1.ToTable("Estimates");

                            b1.HasOne("QuickForecaster.Domain.Entities.Estimate")
                                .WithOne("Estimator")
                                .HasForeignKey("QuickForecaster.Domain.ValueObjects.Contact", "EstimateId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
