using MediatR;
using System.Collections.Generic;

namespace QuickForecaster.Application.Estimates.Queries
{
    public class GetProjectsQuery : IRequest<IList<ProjectDto>>
    {
        public GetProjectsQuery(int clientId)
        {
            ClientId = clientId;
        }

        public int ClientId { get; set; }
    }
}
