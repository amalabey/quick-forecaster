using QuickForecaster.Domain.Entities;
using QuickForecaster.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace QuickForecaster.Application.Clients.Queries
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Contact AccountManager { get; set; }

        public static Expression<Func<Client, ClientDto>> Projection
        {
            get
            {
                return p => new ClientDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    AccountManager = p.AccountManager
                };
            }
        }
    }
}
