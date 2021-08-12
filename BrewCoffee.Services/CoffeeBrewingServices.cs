using BrewCoffee.DTO;
using BrewCoffee.Services.Helpers;
using BrewCoffee.Services.Repository;

namespace BrewCoffee.Services
{
    public interface ICoffeeBrewingServices
    {
        CoffeeDetails GetCoffeeBrewingDetails();
    }

    public class CoffeeBrewingServices : ICoffeeBrewingServices
    {
        private ICoffeeCounter _coffeeCounter;
        private IDateTimeHelper _dateTimeHelper;

        public CoffeeBrewingServices(ICoffeeCounter coffeeCounter, IDateTimeHelper dateTimeHelper)
        {
            _coffeeCounter = coffeeCounter;
            _dateTimeHelper = dateTimeHelper;
        }

        public CoffeeDetails GetCoffeeBrewingDetails()
        {
            var response = new CoffeeDetails();

            if (_coffeeCounter.Count > 3)
            {
                _coffeeCounter.Reset();
                return null;
            }

            var dateTime = _dateTimeHelper.GetDateTimeNow();

            if ((dateTime.Month == 4) && (dateTime.Day == 1))
            {
                return response;
            }

            _coffeeCounter.Brew();
            response = new CoffeeDetails
            {
                Message = "Your piping hot coffee is ready",
                Prepared = dateTime.ToString("o")
            };

            return response;
        }
    }
}
