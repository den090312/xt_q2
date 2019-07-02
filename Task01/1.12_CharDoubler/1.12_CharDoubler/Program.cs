using System;
using System.Text;

namespace _12_CharDoubler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Максимальная длина строки - 254 симвоола");
            Console.WriteLine("");

            Console.WriteLine("Введите первую строку:");
            var firstString = Console.ReadLine();
            Console.WriteLine("");

            Console.WriteLine("Введите вторую строку:");
            var secondString = Console.ReadLine();
            Console.WriteLine("");

            Console.WriteLine("Результирующая строка: "+GetCharDoubledString(firstString, secondString));
            Console.WriteLine("");
        }

        static string GetCharDoubledString(string firstString, string secondStringg)
        {
            string resultString;

            return resultString;
        }

    }
}

