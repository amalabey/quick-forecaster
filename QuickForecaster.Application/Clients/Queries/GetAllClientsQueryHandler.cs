using MediatR;
using QuickForecaster.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace QuickForecaster.Application.Clients.Queries
{
    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, IList<ClientDto>>
    {
        private readonly QuickForecasterDbContext _context;

        public GetAllClientsQueryHandler(QuickForecasterDbContext context)
        {
            _context = context;
        }

        public async Task<IList<ClientDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _context.Clients
                .AsNoTracking()
                .Select(ClientDto.Projection)
                .ToListAsync();

            return clients;
        }
    }
}
