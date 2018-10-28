using MediatR;
using System.Collections.Generic;

namespace QuickForecaster.Application.Estimates.Queries
{
    public class GetSummaryQuery : IRequest<EstimateSummaryDto>
    {
        public GetSummaryQuery(int estimateId, int teamSize)
        {
            EstimateId = estimateId;
            TeamSize = teamSize;
        }

        public int EstimateId { get; set; }

        public int TeamSize { get; set; }
    }
}
