using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _71_DATE_EXISTANCE
{
    public class Program
    {
        private static readonly string dateFormat = "dd-mm-yyyy";

        private static void Main(string[] args)
        {
            Console.WriteLine($"Enter text with date in format: {dateFormat}");

            var userText = Console.ReadLine();

            if (userText.HasDate())
            {
                Console.WriteLine($"There is date in text: {userText}");
            }
            else
            {
                Console.WriteLine($"There is NO date in text: {userText}");
            }
        }
    }

    public static class DateExtensions
    {
        public static bool HasDate(this string userString) 
            => new Regex(@"((0[1-9]|[12]\d|3[01])-(0[1-9]|1[0-2])-[12]\d{3})").IsMatch(userString); 
    }
}
