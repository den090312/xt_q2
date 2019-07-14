using System;

namespace _2._1.ROUND
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var myRound = new Round
            (
                new Point(GetCoordinateFromConsole('X'), GetCoordinateFromConsole('Y')),
                new Point(GetCoordinateFromConsole('X'), GetCoordinateFromConsole('Y'))
            );

            WriteRoundInfo(myRound);
        }

        private static void WriteRoundInfo(Round myRound)
        {
            Console.WriteLine();
            Console.WriteLine($"Координаты центра круга: ({myRound.CenterCoordinates.X},{myRound.CenterCoordinates.Y})");
            Console.WriteLine($"Радиус: {myRound.CircleRadius}");
            Console.WriteLine($"Длина окружности: {myRound.Circumference}");
            Console.WriteLine($"Площадь круга: {myRound.Area}");
            Console.WriteLine();
        }

        public static double GetRadiusFromConsole()
        {
            Console.WriteLine("Радиус");
            double doubleFromConsole;
            bool isInt;

            do
            {
                Console.WriteLine($"Введите положительное число меньше или равно {double.MaxValue}:");
                isInt = double.TryParse(Console.ReadLine(), out doubleFromConsole);

                if (isInt)
                {
                    isInt = doubleFromConsole > 0;
                }
            }
            while (isInt == false);

            return doubleFromConsole;
        }

        public static int GetCoordinateFromConsole(char measure)
        {
            int intFromConsole;
            bool isInt;

            do
            {
                Console.WriteLine($"Ввод координаты. Измерение {measure}");
                Console.WriteLine($"Введите целое число меньше или равно {int.MaxValue}:");
                isInt = int.TryParse(Console.ReadLine(), out intFromConsole);
            }
            while (isInt == false);

            return intFromConsole;
        }
    }
}
