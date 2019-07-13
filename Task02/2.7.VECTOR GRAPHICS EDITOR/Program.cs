using System;
using System.Text;

namespace _2._7.VECTOR_GRAPHICS_EDITOR
{
    public class Program
    {
        private static void Main(string[] args)
        {
            WriteMenu();

            bool inputComplete = false;

            while (!inputComplete)
            {
                int userKey = GetKeyFromConsole();
                if (userKey != 0)
                {
                    CreateSelectedFigure(userKey);
                    Console.WriteLine("Выберите фигуру для вывода:");
                    WriteMenu();
                }
            }
        }

        static void WriteMenu()
        {
            Console.WriteLine("Выберите фигуру для вывода:");
            Console.WriteLine("\t1: Линия");
            Console.WriteLine("\t2: Окружность");
            Console.WriteLine("\t3: Прямоугольник");
            Console.WriteLine("\t4: Круг");
            Console.WriteLine("\t5: Кольцо");
            Console.WriteLine("\t6: ВЫХОД");
        }

        private static void CreateSelectedFigure(int consoleKey)
        {
            switch (consoleKey)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    Environment.Exit(0);
                    break;
            }
        }

        static int GetKeyFromConsole()
        {
            bool inputComplete = false;
            StringBuilder userKeySB = new StringBuilder();
            while (!inputComplete)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                char[] keys = { '1', '2', '3', '4', '5', '6'};

                if (key.Key == ConsoleKey.Enter)
                {
                    inputComplete = true;
                }
                else if (char.IsDigit(key.KeyChar) & (Array.Exists(keys, element => element == key.KeyChar)))
                {
                    if (userKeySB.Length < 1)
                    {
                        userKeySB.Append(key.KeyChar);
                        Console.Write(key.KeyChar);
                    }
                }
            }

            int result;
            if (userKeySB.Length > 0)
            {
                result = int.Parse(userKeySB.ToString());
            }
            else
            {
                result = 0;
            }

            return result;
        }
    }
}
