﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuickForecaster.Persistence;

namespace QuickForecaster.Persistence.Migrations
{
    [DbContext(typeof(QuickForecasterDbContext))]
    [Migration("20181026025057_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("AccountManager");

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

                    b.Property<string>("Estimator");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Estimates");
                });

            modelBuilder.Entity("QuickForecaster.Domain.Entities.BacklogItem", b =>
                {
                    b.HasOne("QuickForecaster.Domain.Entities.Estimate")
                        .WithMany("BacklogItems")
                        .HasForeignKey("EstimateId");
                });

            modelBuilder.Entity("QuickForecaster.Domain.Entities.Estimate", b =>
                {
                    b.HasOne("QuickForecaster.Domain.Entities.Client")
                        .WithMany("Estimates")
                        .HasForeignKey("ClientId");
                });
#pragma warning restore 612, 618
        }
    }
}