﻿using System;
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

            //var matchOneDomain = new Regex(@"[\w\d.-]+@[a-zA-Z\d]+\.[a-zA-Z\d]{2,6}\b").Matches(sb.ToString());

            //var matchOneDomain = new Regex(@"[a-z\d]+([._-][a-z\d]+)*@[a-z\d]+([._-][a-z\d]+)*\.[a-z]{2,6}\b").Matches(sb.ToString());

            //var matchOneDomain = new Regex(@"([a-z\d]+[._-]*[a-z\d]+)+(@[a-z\d]+.[a-z]{2,6}\b)").Matches(sb.ToString());

            //var matchOneDomain = new Regex(@"^[a-z\d]+[._-]*[a-z\d]+@[a-z\d]+.[a-z\d]{2,6}\b$").Matches(sb.ToString());

            //var matchOneDomain = new Regex(@"[a-z\d]+[._-]*[a-z\d]+@[a-z\d]+.[a-z]{2,6}\b").Matches(sb.ToString());

            //var matchAll = new Regex(@"[a-z\d]+[._-]*[a-z\d]+@[a-z\d]+(.[a-z]{2,6}\b)+").Matches(sb.ToString());

            //var emailMatches = new Regex(@"[a-z\d]+[._-]*[a-z\d]+@[a-z\d]+(.[a-z]{2,6}\b)+").Matches(Console.ReadLine());

            var emailMatches = new Regex(@"[a-z\d]+[._-]*[a-z\d]+@[a-z\d]+(\.([a-z\d])+)+").Matches(Console.ReadLine());

            //получить по регулярке список адресов
            //написать функцию, которая будет определять кол-во букв и наличие не-букв в домене верхнего уровня

            WriteMatches(emailMatches);

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
