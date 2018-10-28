using Microsoft.EntityFrameworkCore;
using QuickForecaster.Domain.Entities;
using QuickForecaster.Domain.Enums;
using QuickForecaster.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickForecaster.Persistence.Seed
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasData(
                new Client { Id = 1, Name = "Contoso", },
                new Client { Id = 2, Name = "Fabrikam"},
                new Client { Id = 3, Name = "Wingtip"},
                new Client { Id = 4, Name = "MyDrive"}
            );

            modelBuilder.Entity<Client>()
                .OwnsOne(c => c.AccountManager)
                .HasData(
                new { ClientId = 1, Email = "jdoe@contoso.com", DisplayName = "John Contoso" },
                new { ClientId = 2, Email = "jdoe@fabrikam.com", DisplayName = "John Fabrikam" },
                new { ClientId = 3, Email = "jdoe@wingtip.com", DisplayName = "John Wingtip" },
                new { ClientId = 4, Email = "jdoe@mydrive.com", DisplayName = "John Mydrive" }
            );

            modelBuilder.Entity<Estimate>()
                .HasData(
                new { Id = 1, ClientId = 1, ProjectName = "Contoso Website" },
                new { Id = 2, ClientId = 2, ProjectName = "Fabrikam Mobile App" },
                new { Id = 3, ClientId = 2, ProjectName = "Fabrikam DevOps" }
            );

            modelBuilder.Entity<Estimate>()
                .OwnsOne(e => e.Estimator)
                .HasData(
                new { EstimateId = 1, Email = "jane@Contoso.com", DisplayName = "Jane Contoso" },
                new { EstimateId = 2, Email = "jane@fabrikam.com", DisplayName = "Jane Fabrikam"},
                new { EstimateId = 3, Email = "tom@fabrikam.com", DisplayName = "Tom Fabrikam"}
            );

            modelBuilder.Entity<BacklogItem>().HasData(
                new { Id = 1, EstimateId = 1, Confidence = ConfidenceLevel.High, Task = "Create database schema", OptimisticEstimate = 4.0m, PessimisticEstimate = 6.0m},
                new { Id = 2, EstimateId = 1, Confidence = ConfidenceLevel.Medium, Task = "Create CI/CD pipeline", OptimisticEstimate = 10.0m, PessimisticEstimate = 16.0m },
                new { Id = 3, EstimateId = 1, Confidence = ConfidenceLevel.Low, Task = "Create service layer", OptimisticEstimate = 8.0m, PessimisticEstimate = 10.0m },
                new { Id = 4, EstimateId = 1, Confidence = ConfidenceLevel.High, Task = "Setup dev environment", OptimisticEstimate = 5.0m, PessimisticEstimate = 10.0m },
                new { Id = 5, EstimateId = 1, Confidence = ConfidenceLevel.High, Task = "Acceptance testing", OptimisticEstimate = 15.0m, PessimisticEstimate = 30.0m },
                new { Id = 6, EstimateId = 2, Confidence = ConfidenceLevel.High, Task = "Create home page", OptimisticEstimate = 4.0m, PessimisticEstimate = 6.0m },
                new { Id = 7, EstimateId = 2, Confidence = ConfidenceLevel.Medium, Task = "Create contact-us page", OptimisticEstimate = 10.0m, PessimisticEstimate = 16.0m },
                new { Id = 8, EstimateId = 2, Confidence = ConfidenceLevel.Low, Task = "Browser testing", OptimisticEstimate = 8.0m, PessimisticEstimate = 10.0m },
                new { Id = 9, EstimateId = 2, Confidence = ConfidenceLevel.High, Task = "Client demo", OptimisticEstimate = 5.0m, PessimisticEstimate = 10.0m },
                new { Id = 10, EstimateId = 3, Confidence = ConfidenceLevel.High, Task = "Document deployment guide", OptimisticEstimate = 15.0m, PessimisticEstimate = 30.0m }
            );

        }
    }
}
