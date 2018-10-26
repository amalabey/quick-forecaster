using QuickForecaster.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickForecaster.Domain.Entities
{
    public class Estimate
    {
        public int Id { get; set; }
        public Contact Estimator { get; set; }
        public ICollection<BacklogItem> BacklogItems { get; set; }
    }
}
