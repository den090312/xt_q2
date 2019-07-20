
using System;
using System.Collections.Generic;

namespace _31_LOST
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> numbers = new Queue<int>();

            var n = GetPositiveIntFromConsole();

            for (int i = 1; i <= n; i++)
            {
                numbers.Enqueue(i);
            }

            n = 1;

            do
            {
                if (n == 2)
                {
                    numbers.Dequeue();
                    n = 1;
                }
                else
                {
                    numbers.Enqueue(numbers.Peek());
                    numbers.Dequeue();
                    n = 2;
                }
            }
            while (numbers.Count > 1);

            Console.WriteLine($"LOST: {numbers.Peek()}");
        }

        static int GetPositiveIntFromConsole()
        {
            Console.WriteLine("Количество людей в кругу");

            int manQuantity;

            bool isInt;
            do
            {
                Console.WriteLine($"Введите положительное целое число меньше или равно {int.MaxValue}");
                isInt = int.TryParse(Console.ReadLine(), out manQuantity);

                if (isInt)
                {
                    isInt = manQuantity > 0;
                }
            }
            while (isInt == false);

            return manQuantity;
        }
    }
}
