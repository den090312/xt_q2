
using System;
using System.Collections.Generic;

namespace _31_LOST
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine($"LOST: {GetLost(GetQueue(GetPositiveIntFromConsole()))}");
        }

        private static int GetPositiveIntFromConsole()
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

        private static Queue<int> GetQueue(int n)
        {
            Queue<int> myQueue = new Queue<int>();

            for (int i = 1; i <= n; i++)
            {
                myQueue.Enqueue(i);
            }

            return myQueue;
        }

        private static int GetLost(Queue<int> numbers)
        {
            int step = 1;

            do
            {
                if (step == 2)
                {
                    numbers.Dequeue();
                    step = 1;
                }
                else
                {
                    numbers.Enqueue(numbers.Peek());
                    numbers.Dequeue();
                    step = 2;
                }
            }
            while (numbers.Count > 1);

            return numbers.Peek();
        }
    }
}
