using System;

namespace _1._7.ARRAY_PROCESSING
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxValue = 100;
            int[] myArray = GetRandomArray(maxValue);

            Console.WriteLine($"Массив случайных чисел от 1 до {maxValue}");
            WriteArray(myArray);
            Console.WriteLine();

            Console.WriteLine($"Максимальное значение: {GetMaxValue(myArray)}");
            Console.WriteLine($"Минимальное значение: {GetMinValue(myArray)}");
            Console.WriteLine();

            Console.WriteLine("Отсортированный массив:");
            WriteArray(GetSortedArray(myArray));
        }

        static int[] GetRandomArray(int maxValue)
        {
            Random myRandom = new Random();
            int[] myArray = new int[10];
            for (int i = 0; i <= 9; i++)
            {
                myArray[i] = myRandom.Next(maxValue);
            }

            return myArray;
        }

        static void WriteArray(int[] myArray)
        {
            foreach (int element in myArray)
            {
                Console.WriteLine(element);
            }
        }

        static int GetMaxValue(int[] myArray)
        {
            int max = myArray[0];
            foreach (int element in myArray)
            {
                if (element > max)
                {
                    max = element;
                }
            }

            return max;
        }

        static int GetMinValue(int[] myArray)
        {
            int min = myArray[0];
            foreach (int element in myArray)
            {
                if (element < min)
                {
                    min = element;
                }
            }

            return min;
        }

        static int[] GetSortedArray(int[] myArray)
        {
            int buffer;
            for (int i = 0; i < myArray.Length - 1; i++)
            {
                for (int j = i + 1; j < myArray.Length; j++)
                {
                    if (myArray[j] < myArray[i])
                    {
                        buffer = myArray[i];
                        myArray[i] = myArray[j];
                        myArray[j] = buffer;
                    }
                }
            }

            return myArray;
        }
    }
}
