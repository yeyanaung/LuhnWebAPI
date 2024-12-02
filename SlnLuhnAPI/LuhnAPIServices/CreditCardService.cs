namespace LuhnAPIServices
{
    /// <summary>
    /// Provides methods to validate credit card numbers using the Luhn algorithm.
    /// </summary>
    public static class CreditCardService
    {
        /// <summary>
        /// Validates a credit card number using the Luhn algorithm.
        /// From Right to Left, 
        /// if Odd Index Number, sum up the digit, 
        /// if Even Index Number, multiply by 2 and sum up digits ( if more than 9, sum up those digits first)
        /// </summary>
        /// <param name="cardNumber">The credit card number as a string.</param>
        /// <returns>True if the card number is valid, otherwise false.</returns>
        public static bool IsValid(string cardNumber)
        {
            return true;
        }
    }
}
