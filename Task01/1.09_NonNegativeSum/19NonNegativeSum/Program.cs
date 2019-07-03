using System;

namespace _19NonNegativeSum
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxValue = 100;
            int[] myArray = GetRandomArray(maxValue);

            Console.WriteLine($"Массив случайных чисел от 1 до {maxValue}");
            Console.WriteLine();

            WriteArray(myArray);
            Console.WriteLine();

            Console.WriteLine($"Сумма неотрицательных элементов массива равна: {GetNonNegativeSum(myArray)}");
        }

        static int[] GetRandomArray(int maxValue)
        {
            Random myRandom = new Random();
            int[] myArray = new int[10];
            for (int i = 0; i <= 9; i++)
            {
                myArray[i] = myRandom.Next(-1 * maxValue, maxValue);
            }

            return myArray;
        }

        static int GetNonNegativeSum(int[] myArray)
        {
            int sum = 0;
            foreach (int element in myArray)
            {
                if (element > 0)
                {
                    sum += element;
                }
            }

            return sum;
        }

        static void WriteArray(int[] myArray)
        {
            foreach (int element in myArray)
            {
                Console.WriteLine(element);
            }
        }
    }
}
