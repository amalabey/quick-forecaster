using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickForecaster.Application.Estimates.Commands
{
    public class CreateEstimateCommand : IRequest
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string EstimatorEmail { get; set; }
        public string EstimatorName { get; set; }
    }
}
