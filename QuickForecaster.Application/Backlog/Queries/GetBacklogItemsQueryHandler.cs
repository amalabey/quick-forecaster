using MediatR;
using QuickForecaster.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace QuickForecaster.Application.Backlog.Queries
{
    public class GetBacklogItemsQueryHandler : IRequestHandler<GetBacklogItemsQuery, IList<BacklogItemDto>>
    {
        private readonly QuickForecasterDbContext _context;

        public GetBacklogItemsQueryHandler(QuickForecasterDbContext context)
        {
            _context = context;
        }

        public async Task<IList<BacklogItemDto>> Handle(GetBacklogItemsQuery request, CancellationToken cancellationToken)
        {
            var backlogItems = await _context.BacklogItems
                .AsNoTracking()
                .Include(bi => bi.Estimate)
                .Where(bi => bi.Estimate.Id == request.EstimateId)
                .Select(BacklogItemDto.Projection)
                .ToListAsync();

            return backlogItems;
        }
    }
}
