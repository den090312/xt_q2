using System;
using System.Text;
using System.Text.RegularExpressions;

namespace _73_EMAIL_FINDER
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter text");
            var userString = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Emails found:");
            Console.WriteLine("------------");

            var matchTwoDomains = new Regex(@"[a-zA-Z0-9._-]+@[a-zA-Z0-9-]+\.[a-zA-Z]+(\.[a-zA-Z]{2,6}\b)").Matches(userString);

            WriteMatches(matchTwoDomains);

            var sb = new StringBuilder(userString);

            foreach (var match in matchTwoDomains)
            {
                sb.Replace(match.ToString(), string.Empty);            
            }

            var matchOneDomain = new Regex(@"[a-zA-Z0-9._-]+@[a-zA-Z0-9-]+\.[a-zA-Z]{2,6}\b").Matches(sb.ToString());

            WriteMatches(matchOneDomain);

            Console.WriteLine("------------");
            Console.WriteLine();
        }

        private static void WriteMatches(MatchCollection emailMatches)
        {
            foreach (var match in emailMatches)
            {
                Console.WriteLine(match);
            }
        }
    }
}
