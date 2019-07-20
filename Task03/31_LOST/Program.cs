
using System;
using System.Collections.Generic;

namespace _31_LOST
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine($"LOST: {GetLost(GetQueue(GetHumanQuantity()))}");
        }

        private static int GetHumanQuantity()
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

        private static Queue<int> GetQueue(int humanQuantity)
        {
            Queue<int> myQueue = new Queue<int>();

            for (int i = 1; i <= humanQuantity; i++)
            {
                myQueue.Enqueue(i);
            }

            return myQueue;
        }

        private static int GetLost(Queue<int> humanQueue)
        {
            int step = 1;

            do
            {
                if (step == 2)
                {
                    humanQueue.Dequeue();
                    step = 1;
                }
                else
                {
                    humanQueue.Enqueue(humanQueue.Peek());
                    humanQueue.Dequeue();
                    step = 2;
                }
            }
            while (humanQueue.Count > 1);

            return humanQueue.Peek();
        }
    }
}
