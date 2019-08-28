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

            WriteEmailMatchList
            (
                GetEmailMatchList(new Regex(@"[a-z\d]+[._-]*[a-z\d]+@[a-z\d]+(\.([a-z\d])+)+").Matches(Console.ReadLine()))
            );
        }

        private static List<string> GetEmailMatchList(MatchCollection emailMatchCollection)
        {
            var emailMatchList = new List<string>();

            foreach (var emailMatch in emailMatchCollection)
            {
                var emailStringMatch = emailMatch.ToString();

                if (emailStringMatch.HasCorrectTopLvlDomain())
                {
                    emailMatchList.Add(emailStringMatch);
                }
            }

            return emailMatchList;
        }

        private static void WriteEmailMatchList(List<string> emailMatchList)
        {
            Console.WriteLine();
            Console.WriteLine("Emails found:");
            Console.WriteLine("------------");

            foreach (var emailMatch in emailMatchList)
            {
                Console.WriteLine(emailMatch);
            }

            Console.WriteLine("------------");
            Console.WriteLine();
        }
    }

    public static class StringExtensions
    {
        public static bool HasCorrectTopLvlDomain(this string emailString)
        {
            var lastIndexOf = emailString.LastIndexOf('.');
            var topLvlDomain = emailString.Substring(lastIndexOf + 1, emailString.Length - lastIndexOf - 1);

            return Regex.IsMatch(topLvlDomain, "^[a-z]{2,6}$");
        }
    }
}