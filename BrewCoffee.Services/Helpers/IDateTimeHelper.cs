using System;

namespace BrewCoffee.Services.Helpers
{
    public interface IDateTimeHelper
    {
        DateTime GetDateTimeNow();
    }

    public class DateTimeHelper : IDateTimeHelper
    {
        public DateTime GetDateTimeNow()
        {
            return DateTime.Now;
        }
    }
}
