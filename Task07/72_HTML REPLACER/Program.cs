using System;
using System.Text.RegularExpressions;

namespace _72_HTML_REPLACER
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter text");
            var userText = Console.ReadLine();
            Console.WriteLine(userText.ReplaceHTMLTags("_"));
        }
    }

    public static class StringExtensions
    {
        public static string ReplaceHTMLTags(this string userString, string replaceString) 
            => Regex.Replace(userString, @"<[^<>]+>", replaceString);
    }
}
