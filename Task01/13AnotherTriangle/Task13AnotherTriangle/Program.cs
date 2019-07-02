using System;

namespace Task13AnotherTriangle
{
    class Program
    {
        static void Main(string[] args)
        {
            DrawTreeTriangle(GetNumberOflinesFromConsole());
        }

        static int GetNumberOflinesFromConsole()
        {
            int numberOfLines;
            bool isInt;
            do
            {
                Console.WriteLine("Ввод количества строк в треугольнике:");

                //ограничиваем кол-во строк актуальным размером окна консоли 
                //есть еще неактуальный размер, который не влезает в экран - его в расчет не берем
                int windowEdge = Console.WindowWidth / 2 + 1;
                Console.WriteLine("Введите положительное целое число меньше " + windowEdge);
                isInt = int.TryParse(Console.ReadLine(), out numberOfLines);

                if (isInt)
                {
                    isInt = numberOfLines > 0 & numberOfLines < windowEdge;
                }
            }
            while (isInt == false);

            return numberOfLines;
        }

        static void DrawTreeTriangle(int numberOfLines)
        {
            //рассчитываем отступ слева для вершины треугольника
            int indentValue = ((numberOfLines * 2 - 1) - 1) / 2;

            //инициализируем длину строки треугольника
            int stringLenth = 1;

            for (int i = 1; i <= numberOfLines; i++)
            {
                //выводим отступ
                DrawIndent(indentValue);

                //выводим строку треугольника
                DrawLine(indentValue);

                // получаем длину новой строки
                stringLenth += 2;

                //для каждой новой строки треугольника отступ будет уменьшаться на 1
                indentValue--;
            }
        }

        static void DrawIndent(int indentValue)
        {
            for (int i = 1; i <= indentValue; i++)
                Console.Write(' ');
        }
        
        static void DrawLine(int indentValue)
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
