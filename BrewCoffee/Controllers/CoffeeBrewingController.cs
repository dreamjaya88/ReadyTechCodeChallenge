using BrewCoffee.DTO;
using BrewCoffee.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrewCoffee.Controllers
{
    [ApiController]
    public class CoffeeBrewingController : ControllerBase
    {
        private readonly ICoffeeBrewingServices _services;
        public CoffeeBrewingController(ICoffeeBrewingServices services)
        {
            _services = services;
        }
        /// <returns>Coffee Brewing details</returns>
        /// <response code="200">Returns Coffee Brewing details</response>
        /// <response code="418">On 1 April, Coffee is not brewing</response>  
        /// <response code="503">Every fifth call returns this due to maintainance</response>  
        [HttpGet]
        [Route("brew-coffee")]
        [ProducesResponseType(typeof(BaseResponse<CoffeeDetails>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status418ImATeapot)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public IActionResult GetCoffee()
        {
            var response = _services.GetCoffeeBrewingDetails();

            if (response == null)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, response);
            }

            if (string.IsNullOrWhiteSpace(response.Message))
            {
                return StatusCode(StatusCodes.Status418ImATeapot, response);
            }

            return Ok(response);
        }
    }
}
