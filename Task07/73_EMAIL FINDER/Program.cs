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

            var emailMatchCollection = new Regex(@"[a-z\d]+[._-]*[a-z\d]+@[a-z\d-]+(\.([a-z-])+)+").Matches(Console.ReadLine());

            var emailStringList = new List<string>();

            foreach (var emailMatch in emailMatchCollection)
            {
                var emailMatchString = emailMatch.ToString();

                if (emailMatchString.HasCorrectTopLvlDomain())
                {
                    emailStringList.Add(emailMatchString);
                }
            }

            WriteEmails(emailStringList);
        }

        private static void WriteEmails(List<string> emailStringList)
        {
            Console.WriteLine();
            Console.WriteLine("Emails found:");
            Console.WriteLine("------------");

            foreach (var emailMatchString in emailStringList)
            {
                Console.WriteLine(emailMatchString);
            }

            Console.WriteLine("------------");
            Console.WriteLine();
        }
    }

    public static class StringExtensions
    {
        public static bool HasCorrectTopLvlDomain(this string emailMatchString)
        {
            var lastIndexOf = emailMatchString.LastIndexOf('.');

            var topLvlDomain = emailMatchString.Substring(lastIndexOf + 1, emailMatchString.Length - lastIndexOf - 1);

            return Regex.IsMatch(topLvlDomain, "^[a-z]{2,6}$");
        }
    }
}
