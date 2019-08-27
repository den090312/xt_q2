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

            var listEmailMatches = new List<string>();

            foreach (var emailMatch in emailMatches)
            {
                var stringEmail = emailMatch.ToString();

                if (stringEmail.HasCorrectTopLvlDomain())
                {
                    listEmailMatches.Add(stringEmail);
                }
            }

            WriteEmailMatches(listEmailMatches);
        }

        private static void WriteEmailMatches(List<string> listEmailMatches)
        {
            Console.WriteLine();
            Console.WriteLine("Emails found:");
            Console.WriteLine("------------");

            foreach (var emailMatch in listEmailMatches)
            {
                Console.WriteLine(emailMatch);
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
