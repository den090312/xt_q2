using System;
using System.Text;

namespace _1._12.CHAR_DOUBLER
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Максимальная длина строки - 254 симвоола");
            Console.WriteLine();

            Console.WriteLine("Введите первую строку:");
            var firstString = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Введите вторую строку:");
            var secondString = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine($"Результирующая строка: {GetCharDoubledString(firstString, secondString)}");
            Console.WriteLine();
        }

        static string GetCharDoubledString(string firstString, string secondString)
        {
            var firstCharArray = firstString.ToCharArray();
            var secondCharArray = secondString.ToCharArray();

            var mySB = new StringBuilder();

            for (int i = 0; i <= firstCharArray.Length - 1; i++)
            {
                mySB.Append(firstCharArray[i]);
                if (Array.Exists(secondCharArray, element => element == firstCharArray[i]))
                {
                    mySB.Append(firstCharArray[i]);
                }
            }

            return mySB.ToString().Trim();
        }
    }
}
