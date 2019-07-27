using System;

namespace _45_TO_INT_OR_NOT_TO_INT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var myString = "123456";

            if (myString.IsPositiveInt())
            {
                Console.WriteLine($"{myString} is positive integer");
            }
            else
            {
                Console.WriteLine($"{myString} is NOT positive integer");
            }
        }
    }

    public static class StringExtension
    {
        public static bool IsPositiveInt(this string userString)
        {
            NullCheck(userString);

            if (userString.Length == 1 & userString[0] == '0')
            {
                return false;
            }

            foreach (char symbol in userString)
            {
                if (!char.IsDigit(symbol))
                {
                    return false;
                }
            }

            return true;
        }

        private static void NullCheck(string userString)
        {
            if (userString is null)
            {
                throw new ArgumentNullException($"{nameof(userString)} is null!");
            }
        }
    }
}
