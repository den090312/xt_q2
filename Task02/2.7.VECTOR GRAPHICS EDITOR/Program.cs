﻿using _2._1.ROUND;
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
                    DisplayLine("Линия", GetLineFromConsole());
                    break;
                case 2:
                    DisplayCircle("Окружность", GetCircleFromConsole());
                    break;
                case 3:
                    DisplayRectangle("Прямоугольник", GetRectangleFromConsole());
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

        private static Circle GetCircleFromConsole() => new Circle(GetPointFromConsole("Координаты центра"), GetPointFromConsole("Координаты края"));

        private static Rectangle GetRectangleFromConsole() => new Rectangle(GetLineFromConsole());

        private static Line GetLineFromConsole() => new Line(GetPointFromConsole("Точка 1"), GetPointFromConsole("Точка 2"));

        private static Point GetPointFromConsole(string purpose) => new Point
        (
            _1.ROUND.Program.GetCoordinateFromConsole(purpose, 'X'), 
            _1.ROUND.Program.GetCoordinateFromConsole(purpose, 'Y')
        );

        private static Ring GetRingFromConsole()
        {
            Console.WriteLine();
            Console.WriteLine("Окружность кольца");

            return new Ring(GetCircleFromConsole(), GetPointFromConsole("Координаты внешнего кольца"));
        }

        private static void DisplayRectangle(string figureType, Rectangle rectangle)
        {
            Console.WriteLine();
            Console.WriteLine($"{figureType}");
            Console.WriteLine($"Точка 1: ({rectangle.Line1.Point1.X},{rectangle.Line1.Point1.Y})");
            Console.WriteLine($"Точка 2: ({rectangle.Line1.Point2.X},{rectangle.Line1.Point2.Y})");
            Console.WriteLine($"Точка 3: ({rectangle.Line2.Point1.X},{rectangle.Line2.Point1.Y})");
            Console.WriteLine($"Точка 4: ({rectangle.Line2.Point2.X},{rectangle.Line2.Point2.Y})");
            Console.WriteLine();
        }

        private static void DisplayLine(string figureType, Line line)
        {
            Console.WriteLine();
            Console.WriteLine($"{figureType}");
            Console.WriteLine($"Точка1: ({line.Point1.X},{line.Point1.Y})");
            Console.WriteLine($"Точка2: ({line.Point2.X},{line.Point2.Y})");
            Console.WriteLine();
        }

        private static void DisplayCircle(string figureType, Circle circle)
        {
            Console.WriteLine();
            Console.WriteLine($"{figureType}");
            Console.WriteLine($"Координаты центра: ({circle.CenterCoordinates.X},{circle.CenterCoordinates.Y})");
            Console.WriteLine($"Радиус: {circle.Radius}");
            Console.WriteLine();
        }

        private static void DisplayRing(string figureType, Ring ring)
        {
            Console.WriteLine();
            Console.WriteLine($"{figureType}");
            Console.WriteLine($"Координаты центра: ({ring.Circle.CenterCoordinates.X},{ring.Circle.CenterCoordinates.Y})");
            Console.WriteLine($"Радиус 1: {ring.Circle.Radius}");
            Console.WriteLine($"Радиус 2: {ring.Radius}");
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
