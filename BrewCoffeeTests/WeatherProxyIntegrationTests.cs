using System.Collections.Generic;
using System.Net.Http;
using BrewCoffee.Services.APIProxy;
using BrewCoffee.Services.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace BrewCoffeeTests
{
    [TestFixture(Category = "Integration Tests")]
    public class WeatherProxyIntegrationTests
    {
        private WeatherProxy _proxy;
        private IOptions<ApiSettings> _settings;
        private HttpClient client;

        [SetUp]
        public void Setup()
        {
            _settings = Options.Create(new ApiSettings
            {
                ApiInterfaces = new List<ApiInterface>
                {
                    new ApiInterface
                    {
                        ApiKey = "439d4b804bc8187953eb36d2a8c26a02",
                        Id = "weather",
                        Url = "https://openweathermap.org/data/2.5/weather"
                    }
                }
            });

            client = new HttpClient();
            _proxy = new WeatherProxy(_settings, client);
        }

        [Test]
        public void ShouldTestWeatherApi_When_TestingLiveEndpoint()
        {
            // Act
            var response = _proxy.GetWeather("2147714");

            // Assert
            response.Should().NotBeNull();
        }
    }
}
