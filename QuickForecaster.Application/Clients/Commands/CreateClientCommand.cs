using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickForecaster.Application.Clients.Commands
{
    public class CreateClientCommand : IRequest
    {
        public string Name { get; set; }
        public string AccountManagerEmail { get; set; }
        public string AccountManagerName { get; set; }
    }
}
