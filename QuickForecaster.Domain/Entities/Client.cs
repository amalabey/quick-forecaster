using QuickForecaster.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickForecaster.Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccountManager { get; set; }
        public ICollection<Estimate> Estimates { get; set; }
    }
}
