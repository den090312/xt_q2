using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _73_EMAIL_FINDER
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter text");

            var emailMatches = new Regex(@"[a-z\d]+[._-]*[a-z\d]+@[a-z\d]+(\.([a-z\d])+)+").Matches(Console.ReadLine());

            var listMatches = new List<string>();

            foreach (var match in emailMatches)
            {
                var stringMatch = match.ToString();

                if (stringMatch.HasCorrectTopLvlDomain())
                {
                    listMatches.Add(stringMatch);
                }
            }

            WriteMatches(listMatches);
        }

        private static void WriteMatches(List<string> emailMatches)
        {
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

    public static class StringExtensions
    {
        public static bool HasCorrectTopLvlDomain(this string stringEmail)
        {
            var lastIndexOf = stringEmail.LastIndexOf('.');

            var topLvlDomain = stringEmail.Substring(lastIndexOf + 1, stringEmail.Length - lastIndexOf - 1);

            return Regex.IsMatch(topLvlDomain, "^[a-z]{2,6}$");
        }
    }
}
