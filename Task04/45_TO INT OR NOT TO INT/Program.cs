using System;

namespace _45_TO_INT_OR_NOT_TO_INT
{
    class Program
    {
        static void Main(string[] args)
        {
            var myString = "-123456,6";

            if (myString.IsInt())
            {
                Console.WriteLine($"{myString} is positive integer");
            }
            else
            {
                Console.WriteLine($"{myString} is NOT positive integer");
            }
        }
    }

    internal static class StringExtension
    {
        internal static bool IsInt(this string userString)
        {
            if (userString[0] == '-')
            {
                return false;
            }

            if (userString.Length == 1 & userString[0] == '0')
            {
                return false;
            }

            foreach (char symbol in userString)
            {
                if (symbol < 48 || symbol > 57)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
