using System;
using BrewCoffee.Services;
using BrewCoffee.Services.Helpers;
using BrewCoffee.Services.Repository;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace BrewCoffeeTests
{
    [TestFixture(Category = "Services")]
    public class CoffeeBrewingServicesTests
    {
        private CoffeeBrewingServices _services;
        private IDateTimeHelper _dateTimeHelper;
        private ICoffeeCounter _counter;

        [SetUp]
        public void Setup()
        {
            _counter = Substitute.For<ICoffeeCounter>();
            _dateTimeHelper = Substitute.For<IDateTimeHelper>();
            _services = new CoffeeBrewingServices(_counter, _dateTimeHelper);
        }

        [Test]
        public void ShouldReturnNull_When_CounterHits5()
        {
            // Arrange
            _counter.Count = 4;

            // Act
            var response = _services.GetCoffeeBrewingDetails();

            // Assert
            response.Should().BeNull();
            _counter.Received(1).Reset();
        }

        [Test]
        public void ShouldReturnEmpty_When_DateIsApril1()
        {
            // Arrange
            _counter.Count = 1;
            _dateTimeHelper.GetDateTimeNow().Returns(new DateTime(2021, 04, 1));

            // Act
            var response = _services.GetCoffeeBrewingDetails();

            // Assert
            response.Should().NotBeNull();
            _dateTimeHelper.Received(1).GetDateTimeNow();
            response.Message.Should().BeNullOrWhiteSpace();
            response.Prepared.Should().BeNullOrWhiteSpace();
        }

        [Test]
        public void ShouldReturnBrewingDetails_When_CoffeeBrewingIsCompleted()
        {
            // Arrange
            _counter.Count = 1;
            var date = DateTime.Now;
            _dateTimeHelper.GetDateTimeNow().Returns(date);

            // Act
            var response = _services.GetCoffeeBrewingDetails();

            // Assert
            response.Should().NotBeNull();
            _dateTimeHelper.Received(1).GetDateTimeNow();
            response.Message.Should().NotBeNullOrWhiteSpace();
            response.Message.Should().Be("Your piping hot coffee is ready");
            response.Prepared.Should().NotBeNullOrWhiteSpace();
            response.Prepared.Should().Be(date.ToString("o"));
        }
    }
}
