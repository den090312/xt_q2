using System;

namespace _1._4_X_masTree
{
    class Program
    {
        static void Main(string[] args)
        {
            DrawXMasTree(GetNumberOfFragmentsFromConsole());
        }

        static int GetNumberOfFragmentsFromConsole()
        {
            int numberOfFragments;
            bool isInt;
            do
            {
                Console.WriteLine("Введите количество фрагментов елочки:");

                //ограничиваем кол-во строк актуальным размером окна консоли 
                //есть еще неактуальный размер, который не влезает в экран - его в расчет не берем
                int windowEdge = Console.WindowWidth / 2 + 1;
                Console.WriteLine("Введите положительное целое число меньше " + windowEdge);
                isInt = int.TryParse(Console.ReadLine(), out numberOfFragments);

                if (isInt)
                {
                    isInt = numberOfFragments > 0 & numberOfFragments < windowEdge;
                }
            }
            while (isInt == false);

            return numberOfFragments;
        }

        static void DrawXMasTree(int numberOfFragments)
        {

            //рассчитываем отступ слева для короны елочки
            int leftIndent = (((numberOfFragments * 2 + 1) - 1) / 2);

            //выводим корону елочки
            DrawCrown(leftIndent);

            // инициализируем количество строк фрагмента елочки
            int numberOfStrings = 2;

            while (numberOfFragments > 0)
            {
                //выводим фрагмент елочки
                DrawFragment(leftIndent, numberOfStrings);

                numberOfStrings++;
                numberOfFragments--;
            }
        }

        static void DrawFragment(int leftIndent, int numberOfStrings)
        {
            //инициализируем длину строки фрагмента елочки
            int stringLenth = 1;

            for (int i = 1; i <= numberOfStrings; i++)
            {
                //выводим отступ
                DrawIndent(leftIndent);

                //выводим строку фрагмента елочки
                DrawString(stringLenth);

                //получаем длину новой строки фрагмента елочки
                stringLenth += 2;

                //для каждой новой строки фрагмента елочки отступ будет уменьшаться на 1
                leftIndent--;
            }
        }

        static void DrawCrown(int leftIndent)
        {
            for (int i = 1; i <= leftIndent; i++)
            {
                Console.Write(' ');
            }
            Console.Write('*');
            Console.WriteLine();
        }

        static void DrawIndent(int leftIndent)
        {
            for (int i = 1; i <= leftIndent; i++)
            {
                Console.Write(' ');
            }
        }

        static void DrawString(int stringLenth)
        {
            int j = 1;
            while (j <= stringLenth)
            {
                Console.Write('*');
                j++;
            }
            Console.WriteLine("");
        }
    }
}
