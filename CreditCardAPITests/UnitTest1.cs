using NUnit.Framework;
using API.Controllers;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework.Legacy;

namespace CreditCardAPITests
{
    public class CreditCardControllerTests
    {
        private CreditCardController _controller;

        [SetUp]
        public void Setup()
        {
            // Arrange
            var luhnValidator = new LuhnValidator(); // You may need to inject a mock of ILuhnValidator here
            _controller = new CreditCardController(luhnValidator);
        }

        [Test]
        public void Validate_WithValidCreditCardNumber_ReturnsOkWithTrue()
        {
            // Act
            var result = _controller.Validate("4111111111111111");

            // Assert
            ClassicAssert.IsInstanceOf<OkObjectResult>(result.Result);
            ClassicAssert.IsTrue((bool)((OkObjectResult)result.Result).Value);
        }

        [Test]
        public void Validate_WithInvalidCreditCardNumber_ReturnsOkWithFalse()
        {
            // Act
            var result = _controller.Validate("1234567890123456");

            // Assert
            ClassicAssert.IsInstanceOf<OkObjectResult>(result.Result);
            ClassicAssert.IsFalse((bool)((OkObjectResult)result.Result).Value);
        }

        [Test]
        public void Validate_WithEmptyCreditCardNumber_ReturnsBadRequest()
        {
            // Act
            var result = _controller.Validate("");

            // Assert
            ClassicAssert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
    }
}
