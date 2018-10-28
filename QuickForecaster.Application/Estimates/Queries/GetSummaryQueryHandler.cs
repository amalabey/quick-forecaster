using MediatR;
using QuickForecaster.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuickForecaster.Application.Exceptions;
using QuickForecaster.Common;

namespace QuickForecaster.Application.Estimates.Queries
{
    public class GetSummaryQueryHandler : IRequestHandler<GetSummaryQuery, EstimateSummaryDto>
    {
        private readonly QuickForecasterDbContext _context;
        private readonly IDateTimeProvider _dateTimeProvider;

        public GetSummaryQueryHandler(QuickForecasterDbContext context, IDateTimeProvider dateTimeProvider)
        {
            _context = context;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<EstimateSummaryDto> Handle(GetSummaryQuery request, CancellationToken cancellationToken)
        {
            if(request.EstimateId <= 0)
            {
                throw new ArgumentException("EstimateId is invalid.");
            }

            if(request.TeamSize <= 0)
            {
                throw new ArgumentException("Team size must be a positive number.");
            }

            var backlogItems = await _context.BacklogItems
                .Include(bi => bi.Estimate)
                .Where(bi => bi.Estimate.Id == request.EstimateId)
                .ToListAsync();

            if(backlogItems.Count == 0)
            {
                throw new ValidationException("No backlog items found for the given estimate id");
            }

            var optimisticDurationInDays = (backlogItems.Sum(bi => bi.OptimisticEstimate) / 8) / request.TeamSize;
            var pessimisticDurationInDays = (backlogItems.Sum(bi => bi.PessimisticEstimate) / 8) / request.TeamSize;

            var optimisticEndDate = _dateTimeProvider.Now.AddDays((double)optimisticDurationInDays);
            var pessimisticEndDate = _dateTimeProvider.Now.AddDays((double)pessimisticDurationInDays);

            return new EstimateSummaryDto
            {
                OptimisticDuration = optimisticDurationInDays,
                PessimisticDuration = pessimisticDurationInDays,
                OptimisticEndDate = optimisticEndDate,
                PessimisticEndDate = pessimisticEndDate
            };
        }
    }
}
