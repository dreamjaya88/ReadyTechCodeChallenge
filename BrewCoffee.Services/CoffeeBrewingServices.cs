using BrewCoffee.DTO;
using BrewCoffee.Services.APIProxy;
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
        private IWeatherProxy _proxy;

        public CoffeeBrewingServices(ICoffeeCounter coffeeCounter, IDateTimeHelper dateTimeHelper, IWeatherProxy proxy)
        {
            _coffeeCounter = coffeeCounter;
            _dateTimeHelper = dateTimeHelper;
            _proxy = proxy;
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

            response = new CoffeeDetails
            {
                Message = "Your piping hot coffee is ready",
                Prepared = dateTime.ToString("o")
            };

            var weather = _proxy.GetWeather("2147714");

            if (weather?.Main?.Temp > 30)
            {
                response.Message = "Your refreshing iced coffee is ready";
            }

            _coffeeCounter.Brew();


            return response;
        }
    }
}
