using QuickForecaster.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickForecaster.Domain.Entities
{
    public class BacklogItem
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public ConfidenceLevel Confidence { get; set; }
        public decimal PessimisticEstimate { get; set; }
        public decimal OptimisticEstimate { get; set; }
        public Estimate Estimate { get; set; }
    }
}
