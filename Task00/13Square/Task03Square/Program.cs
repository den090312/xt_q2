using System;

namespace Task03Square
{
    class Program
    {
        static void Main(string[] args)
        {
            DrawStarSquare(GetOddIntFromConsole());
        }

        static int GetOddIntFromConsole()
        {
            int n;
            bool isInt;
            do
            {
                Console.WriteLine("Введите положительное нечетное число больше 1 и меньше 2147483647");
                isInt = int.TryParse(Console.ReadLine(), out n);

                if (isInt)
                {
                    isInt = n > 1 & n % 2 != 0;
                }
            }
            while (isInt == false);

            return n;
        }

        static void DrawStarSquare(int n)
        {
            int centralPoint = (n + 1) / 2;

            for (int i = 1; i <= n; i++)
            {
                DrawStarString(n, i, centralPoint);
                Console.WriteLine();
            }
        }

        static void DrawStarString(int n, int i, int centralPoint)
        {
            for (int j = 1; j <= n; j++)
            {
                if (i == centralPoint & j == centralPoint)
                {
                    Console.Write(' ');
                }
                else
                {
                    Console.Write('*');
                }
            }
        }
    }
}
