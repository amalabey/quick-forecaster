using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickForecaster.Application.Clients.Queries
{
    public class GetAllClientsQuery : IRequest<IList<ClientDto>>
    {
    }
}
