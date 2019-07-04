using System;

namespace _1._08.NO_POSITIVE
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxValue = 100;
            int[,,] myArray = GetRandomArray(maxValue);

            Console.WriteLine($"Трёхмерный массив случайных чисел от {(-1 * maxValue)} до {maxValue}:");
            Console.WriteLine();
            WriteArray(myArray);

            Console.WriteLine("Заменяем положительные элементы на нули:");
            Console.WriteLine();

            WriteArray(SetNoPositiveArray(myArray));
        }

        static void WriteArray(int[,,] myArray)
        {
            for (int x = 0; x <= 4; x++)
            {
                WriteY(x, myArray);
            }
        }

        static void WriteY(int x, int[,,] myArray)
        {
            for (int y = 0; y <= 4; y++)
            {
                WriteZ(x, y, myArray);
            }
            Console.WriteLine();
        }

        static void WriteZ(int x, int y, int[,,] myArray)
        {
            for (int z = 0; z <= 4; z++)
            {
                Console.Write(myArray[x, y, z] + " ");
            }
            Console.WriteLine();
        }

        static int[,,] SetNoPositiveArray(int[,,] myArray)
        {
            for (int x = 0; x <= 4; x++)
            {
                if (myArray[x, 0, 0] > 0)
                {
                    myArray[x, 0, 0] = 0;
                }
                SetNoPositiveY(x, myArray);
            }

            return myArray;
        }

        static void SetNoPositiveY(int x, int[,,] myArray)
        {
            for (int y = 0; y <= 4; y++)
            {
                if (myArray[x, y, 0] > 0)
                {
                    myArray[x, y, 0] = 0;
                }
                SetNoPositiveZ(x, y, myArray);
            }
        }

        static void SetNoPositiveZ(int x, int y, int[,,] myArray)
        {
            for (int z = 0; z <= 4; z++)
            {
                if (myArray[x, y, z] > 0)
                {
                    myArray[x, y, z] = 0;
                }
            }
        }

        static int[,,] GetRandomArray(int maxValue)
        {
            int[,,] myArray = new int[5, 5, 5];
            Random myRandom = new Random();

            for (int x = 0; x <= 4; x++)
            {
                for (int y = 0; y <= 4; y++)
                {
                    for (int z = 0; z <= 4; z++)
                    {
                        myArray[x, y, z] = myRandom.Next(-1 * maxValue, maxValue);
                    }
                }
            }

            return myArray;
        }
    }
}
