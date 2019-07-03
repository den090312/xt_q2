using System;

namespace Task15SumOfNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Задача: Найти сумму всех чисел меньше 1000, кратных 3 или 5");
            Console.WriteLine("Ответ: " + GetSum());
        }

        static int GetSum()
        {
            int sum = 0;

            for (int startNumber = 999; startNumber >= 1; startNumber--)
            {
                if (startNumber % 3 == 0 | startNumber % 5 == 0)
                {
                    sum += startNumber;
                }
            }

            return sum;
        }
    }
}
