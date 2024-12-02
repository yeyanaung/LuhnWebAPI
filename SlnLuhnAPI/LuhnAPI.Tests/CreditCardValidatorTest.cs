namespace LuhnAPI.Tests
{
    using LuhnAPIServices;
    using Xunit;
    public class CreditCardValidatorTest
    {
        [Theory]
        [InlineData("4532015112830366", true)] // Valid Card
        [InlineData("1122334345566677", false)] // Invalid Luhn
        [InlineData("0000000000000000", false)] // Invalid (all zeros)
        [InlineData("453201511283036", false)] // Invalid length (14 digits)

        public void IsValid_Result(string cardNumber, bool expected)
        {
            // Act
            var result = CreditCardService.IsValid(cardNumber);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}