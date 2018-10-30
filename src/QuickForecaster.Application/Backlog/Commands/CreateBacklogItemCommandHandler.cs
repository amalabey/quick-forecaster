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
    public class CreateBacklogItemCommandHandler : IRequestHandler<CreateBackloItemCommand, Unit>
    {
        private readonly QuickForecasterDbContext _context;

        public CreateBacklogItemCommandHandler(QuickForecasterDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateBackloItemCommand request, CancellationToken cancellationToken)
        {
            ///#TODO: Convert to Mediatr validators
            if (request.EstimateId <= 0)
            {
                throw new ArgumentException("Invalid estimate id.");
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

            var estimate = _context.Estimates.FirstOrDefault(e => e.Id == request.EstimateId);
            if(estimate == null)
            {
                throw new ValidationException($"Estimate not found by id {request.EstimateId}");
            }

            var entity = new BacklogItem
            {
                Estimate = estimate,
                Task = request.Task,
                OptimisticEstimate = request.OptimisticEstimate,
                PessimisticEstimate = request.PessimisticEstimate
            };

            _context.BacklogItems.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
