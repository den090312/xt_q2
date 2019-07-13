using _2._1.ROUND;
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
            Console.WriteLine();
        }

        private static TwoPointsFigure GetTwoPointsFigureFromConsole()
        {
            Console.WriteLine();
            Console.WriteLine("Точка 1");
            var point1 = new Point(_1.ROUND.Program.GetCoordinateFromConsole('X'), _1.ROUND.Program.GetCoordinateFromConsole('Y'));
            Console.WriteLine();
            Console.WriteLine("Точка 2");
            var point2 = new Point(_1.ROUND.Program.GetCoordinateFromConsole('X'), _1.ROUND.Program.GetCoordinateFromConsole('Y'));
            Console.WriteLine();
            var userFigure = new TwoPointsFigure(point1, point2);

            return userFigure;
        }

        private static void DisplayTwoPointsFigure(string figureType, TwoPointsFigure twoPointsFigure)
        {
            Console.WriteLine($"Тип фигуры: {figureType}");
            Console.WriteLine($"Точка1: ({twoPointsFigure.Point1.X},{twoPointsFigure.Point1.Y})");
            Console.WriteLine($"Точка2: ({twoPointsFigure.Point2.X},{twoPointsFigure.Point2.Y})");
            Console.WriteLine();
        }

        private static Circle GetCircleFromConsole()
        {
            Console.WriteLine();
            Console.WriteLine("Координаты центра");
            var centerCoordinates = new Point(_1.ROUND.Program.GetCoordinateFromConsole('X'), _1.ROUND.Program.GetCoordinateFromConsole('Y'));
            Radius radius = new Radius(_1.ROUND.Program.GetRadiusFromConsole());
            var myCircle = new Round(centerCoordinates, radius);
            Console.WriteLine();

            return myCircle;
        }
        private static void DisplayCircleFigure(string figureType, Circle circleFigure)
        {
            Console.WriteLine($"Тип фигуры: {figureType}");
            Console.WriteLine($"Координаты центра: ({circleFigure.СenterCoordinates.X},{circleFigure.СenterCoordinates.Y})");
            Console.WriteLine($"Радиус: {circleFigure.CircleRadius.Value}");
            Console.WriteLine();
        }

        private static void CreateSelectedFigure(int consoleKey)
        {
            switch (consoleKey)
            {
                case 1:
                    DisplayTwoPointsFigure("Линия", GetTwoPointsFigureFromConsole());
                    break;
                case 2:
                    DisplayCircleFigure("Окружность", GetCircleFromConsole());
                    break;
                case 3:
                    DisplayTwoPointsFigure("Прямоугольник", GetTwoPointsFigureFromConsole());
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
