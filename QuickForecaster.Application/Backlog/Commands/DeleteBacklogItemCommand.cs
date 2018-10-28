using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickForecaster.Application.Backlog.Commands
{
    public class DeleteBacklogItemCommand : IRequest
    {
        public int Id { get; set; }
    }
}
