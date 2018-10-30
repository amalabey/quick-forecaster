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
    public class DeleteBacklogItemCommandHandler : IRequestHandler<DeleteBacklogItemCommand, Unit>
    {
        private readonly QuickForecasterDbContext _context;

        public DeleteBacklogItemCommandHandler(QuickForecasterDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteBacklogItemCommand request, CancellationToken cancellationToken)
        {
            ///#TODO: Convert to Mediatr validators
            if (request.Id <= 0)
            {
                throw new ArgumentException("Invalid backlog item id.");
            }

            var backlogItem = _context.BacklogItems.FirstOrDefault(e => e.Id == request.Id);
            if(backlogItem == null)
            {
                throw new ValidationException($"Backlog item not found by id {request.Id}");
            }

            _context.BacklogItems.Remove(backlogItem);        

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
