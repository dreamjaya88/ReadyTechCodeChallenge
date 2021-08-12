using System;
using BrewCoffee.Controllers;
using BrewCoffee.DTO;
using BrewCoffee.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;

namespace BrewCoffeeTests
{
    [TestFixture(Category = "Controllers")]
    public class CoffeeBrewingControllerTests
    {
        private CoffeeBrewingController _controller;
        private ICoffeeBrewingServices _services;

        [SetUp]
        public void Setup()
        {
            _services = Substitute.For<ICoffeeBrewingServices>();
            _controller = new CoffeeBrewingController(_services);
        }


        [Test]
        public void ShouldReturn503_When_BrewingIsNotAvailable()
        {
            // Arrange
            _services.GetCoffeeBrewingDetails().ReturnsNull();

            // Act
            var response = _controller.GetCoffee();

            // Assert
            response.Should().NotBeNull();
            _services.Received(1).GetCoffeeBrewingDetails();
            response.Should().BeOfType<ObjectResult>();
            ((ObjectResult)response).StatusCode.Should().Be(StatusCodes.Status503ServiceUnavailable);
        }

        [Test]
        public void ShouldReturn418_When_DateIsApril1()
        {
            // Arrange
            _services.GetCoffeeBrewingDetails().Returns(new CoffeeDetails());

            // Act
            var response = _controller.GetCoffee();

            // Assert
            response.Should().NotBeNull();
            _services.Received(1).GetCoffeeBrewingDetails();
            response.Should().BeOfType<ObjectResult>();
            ((ObjectResult)response).StatusCode.Should().Be(StatusCodes.Status418ImATeapot);
        }

        [Test]
        public void ShouldReturn200_When_CoffeeBrewingIsSuccessFul()
        {
            // Arrange
            var mockedResponse = new CoffeeDetails
            {
                Message = "Your piping hot coffee is ready",
                Prepared = DateTime.Now.ToString("o")
            };

            _services.GetCoffeeBrewingDetails().Returns(mockedResponse);

            // Act
            var response = _controller.GetCoffee();

            // Assert
            response.Should().NotBeNull();
            _services.Received(1).GetCoffeeBrewingDetails();
            response.Should().BeOfType<OkObjectResult>();
            var result = response as OkObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<CoffeeDetails>();
            ((CoffeeDetails)result.Value).Message.Should().NotBeNullOrWhiteSpace();
            ((CoffeeDetails)result.Value).Message.Should().Be(mockedResponse.Message);
            ((CoffeeDetails)result.Value).Prepared.Should().NotBeNullOrWhiteSpace();
            ((CoffeeDetails)result.Value).Prepared.Should().Be(mockedResponse.Prepared);
        }
    }
}