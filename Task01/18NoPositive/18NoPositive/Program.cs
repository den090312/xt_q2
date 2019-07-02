using System;

namespace _18NoPositive
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxValue = 101;
            int minValue = -1 * maxValue;
            int[,,] myArray = GetRandomArray(maxValue, minValue);

            Console.WriteLine("Трёхмерный массив случайных чисел от "+ (minValue + 1) + " до "+ (maxValue - 1) + ":");
            Console.WriteLine("");
            WriteArray(myArray);

            Console.WriteLine("Заменяем положительные элементы на нули:");
            Console.WriteLine("");

            WriteArray(GetNoPositiveArray(myArray));
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

        static int[,,] GetNoPositiveArray(int[,,] myArray)
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
                    myArray[x, y, 0] = 0;
                SetNoPositiveZ(x, y, myArray);
            }
        }

        static void SetNoPositiveZ(int x, int y, int[,,] myArray)
        {
            for (int z = 0; z <= 4; z++)
            {
                if (myArray[x, y, z] > 0)
                    myArray[x, y, z] = 0;
            }
        }

        static int[,,] GetRandomArray(int maxValue, int minValue)
        {
            int[,,] myArray = new int[5, 5, 5];
            Random myRandom = new Random();

            for (int x = 0; x <= 4; i++)
            {
                for (int y = 0; j <= 4; y++)
                {
                    for (int z = 0; z <= 4; z++)
                    {
                        myArray[x, y, z] = myRandom.Next(minValue, maxValue);
                    }                   
                }
            }
                
            return myArray;
        }
    }
}
