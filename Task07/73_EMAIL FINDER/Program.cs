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

<<<<<<< HEAD
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
=======
            WriteEmailMatchList
            (
                GetEmailMatchList(new Regex(@"[a-z\d]+[._-]*[a-z\d]+@[a-z\d]+(\.([a-z\d])+)+").Matches(Console.ReadLine()))
            );
        }

        private static void WriteEmailMatchList(List<string> emailMatchList)
>>>>>>> e03238c3157ff6d7fbf1fc39a3bdeda470167dee
        {
            Console.WriteLine();
            Console.WriteLine("Emails found:");
            Console.WriteLine("------------");

<<<<<<< HEAD
            foreach (var emailMatchString in emailStringList)
            {
                Console.WriteLine(emailMatchString);
=======
            foreach (var emailMatch in emailMatchList)
            {
                Console.WriteLine(emailMatch);
>>>>>>> e03238c3157ff6d7fbf1fc39a3bdeda470167dee
            }

            Console.WriteLine("------------");
            Console.WriteLine();
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
    }

    public static class StringExtensions
    {
<<<<<<< HEAD
        public static bool HasCorrectTopLvlDomain(this string emailMatchString)
        {
            var lastIndexOf = emailMatchString.LastIndexOf('.');

            var topLvlDomain = emailMatchString.Substring(lastIndexOf + 1, emailMatchString.Length - lastIndexOf - 1);
=======
        public static bool HasCorrectTopLvlDomain(this string emailString)
        {
            var lastIndexOf = emailString.LastIndexOf('.');
            var topLvlDomain = emailString.Substring(lastIndexOf + 1, emailString.Length - lastIndexOf - 1);
>>>>>>> e03238c3157ff6d7fbf1fc39a3bdeda470167dee

            return Regex.IsMatch(topLvlDomain, "^[a-z]{2,6}$");
        }
    }
}
