using System;
namespace Lab23.Services
{
    public class UserIdentityService
    {
        const int AMPLITUDE = 555;

        const int MIN_RANDOM_NUMBER = 777;
        const int MAX_RANDOM_NUMBER = 3333;


        private readonly Random random;
        public int RandomIntegerNumber { get; private set; }

        public UserIdentityService()
        {
            random = new Random();
        }

        public void SetNewRandomIntegerNumber()
        {
            RandomIntegerNumber = random.Next(MIN_RANDOM_NUMBER, MAX_RANDOM_NUMBER);
        }

        public bool VerifyUserValue(string userValue, int firstCharsToTake)
        {
            userValue = userValue.Substring(0, firstCharsToTake);
            string realValue = secretFunction().Substring(0, firstCharsToTake);
            return userValue == realValue;
        }

        private string secretFunction()
        {
            double value = Math.Sin(RandomIntegerNumber) * AMPLITUDE;
            return value.ToString();
        }
    }
}