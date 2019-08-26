using System;
using System.Text.RegularExpressions;

namespace _71_DATE_EXISTANCE
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter text with date in format: 'dd-mm-yyyy'");

            var userText = Console.ReadLine();

            if (userText.HasDate())
            {
                Console.WriteLine($"There is date in text: '{userText}'");
            }
            else
            {
                Console.WriteLine($"There is NO date in text: '{userText}'");
            }
        }
    }

    public static class StringExtensions
    {
        public static bool HasDate(this string userString) 
            => new Regex(@"((0[1-9]|[1-2]\d|3[0-1])-(0[1-9]|1[0-2])-\d{4})").IsMatch(userString); 
    }
}
