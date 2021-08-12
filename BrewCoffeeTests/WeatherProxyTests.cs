using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using BrewCoffee.Services.APIProxy;
using BrewCoffee.Services.Helpers;
using BrewCoffee.Services.Models;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NUnit.Framework;

namespace BrewCoffeeTests
{
    [TestFixture(Category = "Proxy Tests")]
    public class WeatherProxyTests
    {
        private WeatherProxy _proxy;
        private IOptions<ApiSettings> _settings;

        [SetUp]
        public void Setup()
        {
            _settings = Options.Create(new ApiSettings
            {
                ApiInterfaces = new List<ApiInterface>
                {
                    new ApiInterface
                    {
                        ApiKey = Guid.NewGuid().ToString(),
                        Id = "weather",
                        Url = "http://localhost:8080"
                    }
                }
            });
        }

        [Test]
        public void ShouldReturnWeatherDetails_When_EnquiredForCity()
        {
            // Arrange
            var mockedRoot = new Root
            {
                Main = new Main
                {
                    Temp = 30.1
                }
            };

            var mockedHttpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(mockedRoot), Encoding.UTF8, "application/json")
            };
            var client = new HttpClient(new MockHttpMessageHandler(mockedHttpResponse));
            _proxy = new WeatherProxy(_settings, client);

            // Act
            var response = _proxy.GetWeather("123123");

            // Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<Root>();
            response.Main.Should().NotBeNull();
            response.Main.Temp.Should().Be(mockedRoot.Main.Temp);
        }

        [Test]
        public void ShouldReturnNoContent_When_EnquiredForCity()
        {
            // Arrange

            var mockedHttpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NoContent,
            };
            var client = new HttpClient(new MockHttpMessageHandler(mockedHttpResponse));
            _proxy = new WeatherProxy(_settings, client);

            // Act
            var response = _proxy.GetWeather("123123");

            // Assert
            response.Should().BeNull();
        }
    }
}
