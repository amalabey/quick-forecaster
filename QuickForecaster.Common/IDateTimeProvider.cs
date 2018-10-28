using System;


namespace QuickForecaster.Common
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
