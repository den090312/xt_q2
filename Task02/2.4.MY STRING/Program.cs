using System;

namespace _2._4.MY_STRING
{
    class Program
    {
        private static void Main(string[] args)
        {
            var myString = new MyString("строка12345");

            if (myString.TryFind('3', out int index))
            {
                Console.WriteLine($"Значение найдено, индекс равен {index}");
            }
            else
            {
                Console.WriteLine("Значение не найдено");
            }
        }
    }
}
