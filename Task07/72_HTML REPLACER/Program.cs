using System;
using System.Text.RegularExpressions;

namespace _72_HTML_REPLACER
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter text");
            Console.WriteLine(ReplaceHTMLTags(Console.ReadLine()));
        }

        public static string ReplaceHTMLTags(string userString) => Regex.Replace(userString, @"<\s*[^<]*>", "_");
    }
}
