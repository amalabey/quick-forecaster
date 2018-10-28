using MediatR;
using QuickForecaster.Application.Exceptions;
using QuickForecaster.Domain.Entities;
using QuickForecaster.Domain.ValueObjects;
using QuickForecaster.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace QuickForecaster.Application.Backlog.Commands
{
    public class UpdateBacklogItemCommandHandler : IRequestHandler<UpdateBackloItemCommand, Unit>
    {
        private readonly QuickForecasterDbContext _context;

        public UpdateBacklogItemCommandHandler(QuickForecasterDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateBackloItemCommand request, CancellationToken cancellationToken)
        {
            ///#TODO: Convert to Mediatr validators
            if (request.BacklogItemId <= 0)
            {
                throw new ArgumentException("Invalid backlog item id.");
            }

            if(String.IsNullOrEmpty(request.Task))
            {
                throw new ArgumentException("Task name cannot be null or empty.");
            }

            if(request.PessimisticEstimate <= 0 || request.OptimisticEstimate <= 0)
            {
                throw new ValidationException("Both optimistic and pessimistic estimates should be > 0.");
            }

            if(request.OptimisticEstimate > request.PessimisticEstimate)
            {
                throw new ValidationException("Optimistic estimate cannot be higher than pessimistic estimate");
            }

            var backlogItem = _context.BacklogItems.FirstOrDefault(e => e.Id == request.BacklogItemId);
            if(backlogItem == null)
            {
                throw new ValidationException($"Backlog item not found by id {request.BacklogItemId}");
            }

            backlogItem.Task = request.Task;
            backlogItem.Confidence = request.Confidence;
            backlogItem.OptimisticEstimate = request.OptimisticEstimate;
            backlogItem.PessimisticEstimate = request.PessimisticEstimate;           

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
