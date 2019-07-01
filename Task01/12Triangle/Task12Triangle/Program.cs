using System;

namespace Task12Triangle
{
    class Program
    {
        static void Main(string[] args)
        {
            DrawTriangle(GetPositiveIntFromConsole());
        }

        static int GetPositiveIntFromConsole()
        {
            int n;
            bool isInt;
            do
            {
                Console.WriteLine("Введите положительное число меньше 2147483647");
                isInt = int.TryParse(Console.ReadLine(), out n);

                if (isInt)
                {
                    isInt = n > 0;
                }
            }
            while (isInt == false);

            return n;
        }

        static void DrawTriangle(int n)
        {
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    Console.Write('*');
                }
                Console.WriteLine();
            }
        }
    }
}
