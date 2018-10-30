using QuickForecaster.Domain.Entities;
using QuickForecaster.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace QuickForecaster.Application.Backlog.Queries
{
    public class BacklogItemDto
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public ConfidenceLevel Confidence { get; set; }
        public decimal PessimisticEstimate { get; set; }
        public decimal OptimisticEstimate { get; set; }

        public static Expression<Func<BacklogItem, BacklogItemDto>> Projection
        {
            get
            {
                return e => new BacklogItemDto
                {
                    Id = e.Id,
                    Confidence = e.Confidence,
                    OptimisticEstimate = e.OptimisticEstimate,
                    PessimisticEstimate = e.PessimisticEstimate,
                    Task = e.Task
                };
            }
        }
    }
}
