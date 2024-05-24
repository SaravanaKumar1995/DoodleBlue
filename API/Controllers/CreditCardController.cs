using Microsoft.AspNetCore.Mvc;
using API.Services;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class CreditCardController : ControllerBase
 {
        private readonly ILuhnValidator _luhnValidator;

        public CreditCardController(ILuhnValidator luhnValidator)
        {
            _luhnValidator = luhnValidator;
        }


        /// <summary>
        /// Validates a credit card number using the Luhn algorithm.
        /// </summary>
        /// <param name="creditCardNumber">The credit card number to validate.</param>
        /// <returns>Boolean indicating whether the credit card number is valid.</returns>
        [HttpPost("validate")]
        public ActionResult<bool> Validate([FromBody] string creditCardNumber)
        {
           try
            {
                if (string.IsNullOrWhiteSpace(creditCardNumber))
                {
                    return BadRequest("Credit card number is required.");
                }

                bool isValid = _luhnValidator.IsValid(creditCardNumber);
                return Ok(isValid);
            }
            catch (FormatException ex)
            {
                return BadRequest($"Invalid input format: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    
}
