using System;

namespace _1._3.SQUARE
{
    class Program
    {
        private static void Main(string[] args)
        {
            DrawSquare(GetPositiveOddIntFromConsole());
        }

        private static int GetPositiveOddIntFromConsole()
        {
            int n;
            bool isInt;
            do
            {
                Console.WriteLine("Введите нечетное число больше 2 и меньше 2147483648");
                isInt = int.TryParse(Console.ReadLine(), out n);

                if (isInt)
                {
                    isInt = n > 2 & n % 2 != 0;
                }
            }
            while (isInt == false);

            return n;
        }

        private static void DrawSquare(int n)
        {
            int centralPoint = (n - 1) / 2;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
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

                Console.WriteLine();
            }
        }
    }
}
