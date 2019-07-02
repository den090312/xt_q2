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
            int indentValue = (((numberOfFragments * 2 + 1) - 1) / 2);

            //выводим корону елочки
            DrawCrown(indentValue);

            //инициализиурем количество строк фрагмента елочки
            int numberOfLines = 2;

            while (numberOfFragments > 0)
            {
                //выводим фрагмент елочки
                DrawFragment(indentValue, numberOfLines);

                numberOfLines++;
                numberOfFragments--;
            }
        }

        static void DrawFragment(int indentValue, int numberOfLines)
        {
            //инициализируем длину строки фрагмента елочки
            int stringLenth = 1;

            for (int i = 1; i <= numberOfLines; i++)
            {
                //выводим отступ
                DrawIndent(indentValue);

                //выводим строку фрагмента елочки
                DrawString(stringLenth);

                // получаем длину новой строки
                stringLenth += 2;

                //для каждой новой строки фрагмента елочки отступ будет уменьшаться на 1
                indentValue--;
            }
        }

        static void DrawCrown(int indentValue)
        {
            for (int i = 1; i <= indentValue; i++)
            {
                Console.Write(' ');
            }
            Console.Write('*');
            Console.WriteLine();
        }

        static void DrawIndent(int indentValue)
        {
            for (int i = 1; i <= indentValue; i++)
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
