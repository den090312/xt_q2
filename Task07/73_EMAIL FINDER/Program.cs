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

            WriteEmailMatches
            (
                new Regex(@"[a-z\d]+[._-]*[a-z\d]+@[a-z\d]+(.[a-z]{2,6}\b)+").Matches(Console.ReadLine())
            );

        }

        private static void WriteEmailMatches(MatchCollection emailMatches)
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
}
