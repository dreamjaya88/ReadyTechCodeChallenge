using BrewCoffee.Services.Helpers;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace BrewCoffeeTests
{
    [TestFixture(Category = "Helpers")]
    public class DateTimeHelperTests
    {

        [Test]
        public void ShouldReturnDateTime_When_Get()
        {
            // Arrange
            var helper = new DateTimeHelper();

            // Act
            var response = helper.GetDateTimeNow();

            // Assert
            response.Year.Should().Be(DateTime.Now.Year);
            response.Month.Should().Be(DateTime.Now.Month);
            response.Day.Should().Be(DateTime.Now.Day);
        }
    }
}
