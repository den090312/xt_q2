using System;

namespace _1._04.X_MAS_TREE
{
    class Program
    {
        static void Main(string[] args)
        {
            DrawXMasTree(GetNumberOfFragmentsFromConsole());
        }

        static int GetNumberOfFragmentsFromConsole()
        {
            //количество фрагментов елочки
            int quantityOfFragments;

            bool isInt;
            do
            {
                Console.WriteLine("Введите количество фрагментов елочки:");

                //ограничиваем кол-во строк актуальным размером окна консоли 
                //есть еще неактуальный размер, который не влезает в экран - его в расчет не берем
                int windowEdge = Console.WindowWidth / 2 + 1;
                Console.WriteLine($"Введите положительное целое число меньше {windowEdge}");
                isInt = int.TryParse(Console.ReadLine(), out quantityOfFragments);

                if (isInt)
                {
                    isInt = quantityOfFragments > 0 & quantityOfFragments < windowEdge;
                }
            }
            while (isInt == false);

            return quantityOfFragments;
        }

        static void DrawXMasTree(int quantityOfFragments)
        {
            //рассчитываем отступ слева для короны елочки
            int leftIndent = (((quantityOfFragments * 2 + 1) - 1) / 2);

            //выводим корону елочки
            DrawCrown(leftIndent);

            //выводим остальные фрагменты елочки
            DrawTreeFragments(leftIndent, quantityOfFragments);
        }

        static void DrawTreeFragments(int leftIndent, int quantityOfFragments)
        {
            // инициализируем количество строк фрагмента елочки
            int quantityOfStrings = 2;

            while (quantityOfFragments > 0)
            {
                //выводим фрагмент елочки
                DrawFragment(leftIndent, quantityOfStrings);

                quantityOfStrings++;
                quantityOfFragments--;
            }
        }

        static void DrawFragment(int leftIndent, int quantityOfStrings)
        {
            //инициализируем длину строки фрагмента елочки
            int stringLenth = 1;

            for (int i = 1; i <= quantityOfStrings; i++)
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
            Console.WriteLine();
        }
    }
}
