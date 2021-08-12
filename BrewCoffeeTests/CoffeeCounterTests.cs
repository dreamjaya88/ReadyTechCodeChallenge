using BrewCoffee.Services.Repository;
using FluentAssertions;
using NUnit.Framework;

namespace BrewCoffeeTests
{
    [TestFixture(Category = "Repository")]
    public class CoffeeCounterTests
    {
        private CoffeeCounter _coffeeCounter;

        [SetUp]
        public void Setup()
        {
            _coffeeCounter = new CoffeeCounter();
        }

        [Test]
        public void ShouldReturnDefaultCount_When_Initialised()
        {
            // Act
            var response = _coffeeCounter.Count;

            // Assert
            response.Should().Be(0);
        }

        [Test]
        public void ShouldResetCounterTo0_When_Reset()
        {
            // Arrange
            _coffeeCounter.Count = 4;

            // Act
            _coffeeCounter.Reset();

            // Assert
            _coffeeCounter.Count.Should().Be(0);
        }

        [Test]
        public void ShouldIncrementCounterBy1_When_Reset()
        {
            // Arrange
            _coffeeCounter.Count = 3;

            // Act
            _coffeeCounter.Brew();

            // Assert
            _coffeeCounter.Count.Should().Be(4);
        }
    }
}
