using System;
using System.Text;

namespace Application.Helpers
{
    public static class RandomDataGenerator
    {
        private static readonly Random _random = new Random();
        private const string Letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Digits = "0123456789";

        public static string GenerateRandomString(int minLength, int maxLength)
        {
            int length = _random.Next(minLength, maxLength + 1);
            var sb = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                sb.Append(Letters[_random.Next(Letters.Length)]);
            }

            return sb.ToString();
        }

        public static string GenerateRandomPhoneNumber()
        {
            var sb = new StringBuilder(9);

            for (int i = 0; i < 9; i++)
            {
                sb.Append(Digits[_random.Next(Digits.Length)]);
            }

            return sb.ToString();
        }

        public static string GenerateRandomEmail()
        {
            string localPart = GenerateRandomString(8, 10);
            return $"{localPart}@gmail.com";
        }

        public static string GenerateRandomWebsite()
        {
            string domainName = GenerateRandomString(5, 9);
            return $"www.{domainName}.com";
        }

        public static string GenerateRandomTaskDescription()
        {
            return GenerateRandomString(15, 20);
        }
    }
}
