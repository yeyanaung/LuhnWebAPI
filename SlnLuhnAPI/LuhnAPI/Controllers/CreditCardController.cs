
using LuhnAPIServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LuhnAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        private readonly ILogger<CreditCardController> _logger;

        public CreditCardController(ILogger<CreditCardController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Validates a credit card number using the Luhn algorithm.
        /// </summary>
        /// <param name="cardNumber">The credit card number as a string.</param>
        /// <returns>A result indicating if the card number is valid.</returns>
        [HttpGet("validatecardnumber")]
        public IActionResult ValidateCardNumber([FromQuery][Required] string cardNumber)
        {
            _logger.LogInformation("Received card number validation request: {CardNumber}", cardNumber);

            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                _logger.LogWarning("Card number is missing in the request.");
                return BadRequest(new { message = "Card number is required." });
            }

            if (!IsNumeric(cardNumber) || cardNumber.Length < 15 || cardNumber.Length > 16)
            {
                _logger.LogWarning("Invalid card number: {CardNumber}", cardNumber);
                return BadRequest(new { message = "Invalid credit card number. It must be a numeric string of 15 or 16 digits." });
            }

            bool isValid = CreditCardService.IsValid(cardNumber);
            _logger.LogInformation("Validation result for card number {CardNumber}: {IsValid}", cardNumber, isValid);

            return Ok(new { cardNumber, isValid });
        }

        private bool IsNumeric(string value) => value.All(char.IsDigit);
    }
}
