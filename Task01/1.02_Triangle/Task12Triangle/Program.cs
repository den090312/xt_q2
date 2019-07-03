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
                //ограничиваем кол-во строк актуальным размером окна консоли 
                //есть еще неактуальный размер, который не влезает в экран - его в расчет не берем
                int windowEdge = Console.WindowWidth + 1;
                Console.WriteLine("Введите положительное целое число меньше " + windowEdge);
                isInt = int.TryParse(Console.ReadLine(), out n);

                if (isInt)
                {
                    isInt = n > 0 & n < windowEdge; ;
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
