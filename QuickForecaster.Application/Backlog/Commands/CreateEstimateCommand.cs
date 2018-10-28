using MediatR;
using QuickForecaster.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickForecaster.Application.Backlog.Commands
{
    public class CreateBackloItemCommand : IRequest
    {
        public int EstimateId { get; set; }
        public string Task { get; set; }
        public ConfidenceLevel Confidence { get; set; }
        public decimal PessimisticEstimate { get; set; }
        public decimal OptimisticEstimate { get; set; }
    }
}
