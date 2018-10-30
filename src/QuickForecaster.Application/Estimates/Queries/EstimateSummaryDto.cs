using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickForecaster.Application.Estimates.Queries
{
    public class EstimateSummaryDto
    {
        public decimal OptimisticDuration { get; set; }
        public decimal PessimisticDuration { get; set; }
        public DateTime OptimisticEndDate { get; set; }
        public DateTime PessimisticEndDate { get; set; }
    }
}
