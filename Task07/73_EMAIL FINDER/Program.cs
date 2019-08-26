using System;
using System.Text.RegularExpressions;

namespace _73_EMAIL_FINDER
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter text");

            var emailMatches = new Regex(@"[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9]+(?:\.[a-zA-Z]{2,6}\b)+").Matches(Console.ReadLine());

            Console.WriteLine();
            Console.WriteLine("Emails found:");
            Console.WriteLine("------------");

            foreach (var match in emailMatches)
            {
                Console.WriteLine(match);
            }

            Console.WriteLine("------------");
            Console.WriteLine();
        }
    }
}
