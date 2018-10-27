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

namespace QuickForecaster.Application.Clients.Commands
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Unit>
    {
        private readonly QuickForecasterDbContext _context;

        public CreateClientCommandHandler(QuickForecasterDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            Validate(request);

            var entity = new Client
            {
                Name = request.Name,
                AccountManager = new Contact(request.AccountManagerEmail, request.AccountManagerName)
            };

            _context.Clients.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private void Validate(CreateClientCommand request)
        {
            if (String.IsNullOrEmpty(request.Name))
            {
                throw new ValidationException("Client Name cannot be null or empty.");
            }
        }
    }
}
