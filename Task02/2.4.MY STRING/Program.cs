using System;

namespace _2._4.MY_STRING
{
    class Program
    {
        private static void Main(string[] args)
        {
            var myString = new MyString("строка2957667349000011");

            WriteMyString(myString.Sort());

            if (myString.TryFind('3', out int index))
            {
                Console.WriteLine($"Значение найдено, индекс равен {index}");
            }
            else
            {
                Console.WriteLine("Значение не найдено");
            }
        }

        private static void WriteMyString(MyString myString)
        {
            Console.WriteLine("Отсортированный массив:");

            var charArray = myString.ToCharArray();

            foreach (char element in charArray)
            {
                Console.Write($"{element} ");
            }

            Console.WriteLine();
        }
    }
}
