using _2._1.ROUND;
using _2._6.RING;
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
                    DisplaySelectedFigure(userKey);
                    WriteMenu();
                }
            }
        }

        private static void DisplaySelectedFigure(int consoleKey)
        {
            switch (consoleKey)
            {
                case 1:
                    DisplayTwoPoints("Линия", GetTwoPointsFromConsole());
                    break;
                case 2:
                    DisplayCircle("Окружность", GetCircleFromConsole());
                    break;
                case 3:
                    DisplayTwoPoints("Прямоугольник", GetTwoPointsFromConsole());
                    break;
                case 4:
                    DisplayCircle("Круг", GetCircleFromConsole());
                    break;
                case 5:
                    DisplayRing("Кольцо", GetRingFromConsole());
                    break;
                case 6:
                    Environment.Exit(0);
                    break;
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

        private static Line GetTwoPointsFromConsole()
        {
            Console.WriteLine();
            Console.WriteLine("Точка 1");
            var point1 = new Point(_1.ROUND.Program.GetCoordinateFromConsole('X'), _1.ROUND.Program.GetCoordinateFromConsole('Y'));
            Console.WriteLine();
            Console.WriteLine("Точка 2");
            var point2 = new Point(_1.ROUND.Program.GetCoordinateFromConsole('X'), _1.ROUND.Program.GetCoordinateFromConsole('Y'));
            Console.WriteLine();
            var userFigure = new Line(point1, point2);

            return userFigure;
        }

        private static void DisplayTwoPoints(string figureType, Line twoPointsFigure)
        {
            Console.WriteLine($"Тип фигуры: {figureType}");
            Console.WriteLine($"Точка1: ({twoPointsFigure.Point1.X},{twoPointsFigure.Point1.Y})");
            Console.WriteLine($"Точка2: ({twoPointsFigure.Point2.X},{twoPointsFigure.Point2.Y})");
            Console.WriteLine();
        }

        private static Circle GetCircleFromConsole()
        {
            //Console.WriteLine();
            //Console.WriteLine("Координаты центра");
            //var centerCoordinates = new Point(_1.ROUND.Program.GetCoordinateFromConsole('X'), _1.ROUND.Program.GetCoordinateFromConsole('Y'));
            //Console.WriteLine("Координаты края");
            //var edgeCoordinates = new Point(_1.ROUND.Program.GetCoordinateFromConsole('X'), _1.ROUND.Program.GetCoordinateFromConsole('Y'));

            //Radius radius = new Radius(_1.ROUND.Program.GetRadiusFromConsole());
            //var userCircle = new Circle(centerCoordinates, radius);
            //Console.WriteLine();

            var userFigure = GetTwoPointsFromConsole();


            return userCircle;
        }
        private static void DisplayCircle(string figureType, Circle circleFigure)
        {
            Console.WriteLine($"Тип фигуры: {figureType}");
            Console.WriteLine($"Координаты центра: ({circleFigure.СenterCoordinates.X},{circleFigure.СenterCoordinates.Y})");
            Console.WriteLine($"Радиус: {circleFigure.CircleRadius.Value}");
            Console.WriteLine();
        }

        private static Ring GetRingFromConsole()
        {
            Console.WriteLine();
            var centerCoordinates = new Point(_1.ROUND.Program.GetCoordinateFromConsole('X'), _1.ROUND.Program.GetCoordinateFromConsole('Y'));
            Radius innerRadius = new Radius(_1.ROUND.Program.GetRadiusFromConsole());
            Radius outerRadius = new Radius(_1.ROUND.Program.GetRadiusFromConsole());
            var userRing = new Ring(centerCoordinates, innerRadius, outerRadius);
            Console.WriteLine();

            return userRing;
        }

        private static void DisplayRing(string figureType, Ring ringFigure)
        {
            Console.WriteLine($"Тип фигуры: {figureType}");
            Console.WriteLine($"Координаты центра: ({ringFigure.СenterCoordinates.X},{ringFigure.СenterCoordinates.Y})");
            Console.WriteLine($"Внутренний радиус: {ringFigure.OuterRadius.Value}");
            Console.WriteLine($"Внешний радиус: {ringFigure.InnerRadius.Value}");
            Console.WriteLine();
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
