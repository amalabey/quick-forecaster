using MediatR;
using System.Collections.Generic;

namespace QuickForecaster.Application.Backlog.Queries
{
    public class GetBacklogItemsQuery : IRequest<IList<BacklogItemDto>>
    {
        public GetBacklogItemsQuery(int estimateId)
        {
            EstimateId = estimateId;
        }

        public int EstimateId { get; set; }
    }
}
