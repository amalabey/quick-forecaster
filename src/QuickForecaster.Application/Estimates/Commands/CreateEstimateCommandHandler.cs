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

namespace QuickForecaster.Application.Estimates.Commands
{
    public class CreateEstimateCommandHandler : IRequestHandler<CreateEstimateCommand, Unit>
    {
        private readonly QuickForecasterDbContext _context;

        public CreateEstimateCommandHandler(QuickForecasterDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateEstimateCommand request, CancellationToken cancellationToken)
        {
            if (request.ClientId <= 0)
            {
                throw new ValidationException("Invalid client id.");
            }

            if (String.IsNullOrEmpty(request.Name))
            {
                throw new ValidationException("Project Name cannot be null or empty.");
            }

            var client = _context.Clients.FirstOrDefault(c => c.Id == request.ClientId);
            if(client == null)
            {
                throw new ValidationException($"Client not found by id {request.ClientId}");
            }

            var entity = new Estimate
            {
                ProjectName = request.Name,
                Estimator = String.IsNullOrEmpty(request.EstimatorName) ? null : new Contact(request.EstimatorEmail, request.EstimatorName),
                Client = client
            };

            _context.Estimates.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
