using System;

namespace _1._10._2D_ARRAY
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] myArray = GetRandom2DArray();

            Console.WriteLine("Двумерный массив случайных чисел:");
            Console.WriteLine();

            Write2DArray(myArray);
            Console.WriteLine();

            Console.WriteLine($"Сумма элементов массива, стоящих на чётных позициях, равна {GetSumOddPosElements(myArray)}");
        }

        static int[,] GetRandom2DArray()
        {
            Random myRandom = new Random();
            int[,] myArray = new int[10, 10];
            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    myArray[i, j] = myRandom.Next(0, 9);
                }
            }

            return myArray;
        }

        static void Write2DArray(int[,] myArray)
        {
            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    Console.Write(myArray[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        static int GetSumOddPosElements(int[,] myArray)
        {
            int sum = 0;
            for (int i = 0; i < myArray.Length; i++)
            {
                for (int j = 0; j < myArray.Length; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        sum += myArray[i, j];
                    }
                }
            }

            return sum;
        }
    }
}
