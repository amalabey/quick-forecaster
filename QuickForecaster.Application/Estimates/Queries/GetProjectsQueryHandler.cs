using MediatR;
using QuickForecaster.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace QuickForecaster.Application.Estimates.Queries
{
    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, IList<ProjectDto>>
    {
        private readonly QuickForecasterDbContext _context;

        public GetProjectsQueryHandler(QuickForecasterDbContext context)
        {
            _context = context;
        }

        public async Task<IList<ProjectDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _context.Estimates
                .AsNoTracking()
                .Include(e => e.Client)
                .Where(e => e.Client.Id == request.ClientId)
                .Select(ProjectDto.Projection)
                .ToListAsync();

            return projects;
        }
    }
}
