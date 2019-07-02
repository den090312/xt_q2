using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _111AverageStringLength
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку. Максимальная длина - 254 симвоола");
            StringBuilder myStringBuilder = new StringBuilder(Console.ReadLine());

            RemovePunctuation(myStringBuilder);

            var myString = myStringBuilder.ToString();
            var stringLength = myString.Replace(" ", "").Length;
            var wordsArray = myStringBuilder.ToString().Split(' ');

            Console.WriteLine("Средняя длина слова: "+stringLength / wordsArray.Length);
        }

        static void RemovePunctuation(StringBuilder myStringBuilder)
        {
            string[] punctuationMarks = { ".", "," , ":", ";", "!", "?", "'", "-", "—", "{", "}", "[", "]", "(", ")", "«", "»", "<", ">", "\"" };
            foreach (string element in punctuationMarks)
            {
                myStringBuilder.Replace(element, "");
            }
        }
    }
}
