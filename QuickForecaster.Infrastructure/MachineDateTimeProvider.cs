using QuickForecaster.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickForecaster.Infrastructure
{
    public class MachineDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
