
using System;
using System.Collections.Generic;

namespace _31_LOST
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine($"LOST: {Lost.GetHumanQuantity().GetQueue().GetLost()}");
        }
    }

    internal static class Lost
    {
        internal static int GetHumanQuantity()
        {
            Console.WriteLine("Количество людей в кругу");

            int humanQuantity;

            bool isInt;
            do
            {
                Console.WriteLine($"Введите положительное целое число меньше или равно {int.MaxValue}");
                isInt = int.TryParse(Console.ReadLine(), out humanQuantity);

                if (isInt)
                {
                    isInt = humanQuantity > 0;
                }
            }
            while (isInt == false);

            return humanQuantity;
        }

        internal static Queue<int> GetQueue(this int humanQuantity)
        {
            Queue<int> humanQueue = new Queue<int>(humanQuantity);

            for (int i = 1; i <= humanQuantity; i++)
            {
                humanQueue.Enqueue(i);
            }

            return humanQueue;
        }

        internal static int GetLost(this Queue<int> humanQueue)
        {
            bool removeNext = false;

            do
            {
                switch (removeNext)
                {
                    case false:
                        humanQueue.Enqueue(humanQueue.Peek());
                        humanQueue.Dequeue();
                        removeNext = true;
                        break;
                    default:
                        humanQueue.Dequeue();
                        removeNext = false;
                        break;
                }
            }
            while (humanQueue.Count > 1);

            return humanQueue.Peek();
        }
    }
}
