using System;
using System.Text.RegularExpressions;

namespace _72_HTML_REPLACER
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter text");
            Console.WriteLine(RemoveHTMLTags(Console.ReadLine()));
        }

        public static string RemoveHTMLTags(string userString) => Regex.Replace(userString, @"<\s*[^<]*>", "_");
    }
}
