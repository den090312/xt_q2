using System;

namespace _2._1.ROUND
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var myRound = new Round
            (
                new Point(GetCoordinateFromConsole("Центр круга", 'X'), GetCoordinateFromConsole("Центр круга", 'Y')),
                new Point(GetCoordinateFromConsole("Край круга", 'X'), GetCoordinateFromConsole("Край круга", 'Y'))
            );

            WriteRoundInfo(myRound);
        }

        private static void WriteRoundInfo(Round myRound)
        {
            Console.WriteLine();
            Console.WriteLine($"Координаты центра круга: ({myRound.CenterCoordinates.X},{myRound.CenterCoordinates.Y})");
            Console.WriteLine($"Радиус: {myRound.Radius}");
            Console.WriteLine($"Длина окружности: {myRound.Circumference}");
            Console.WriteLine($"Площадь круга: {myRound.Area}");
            Console.WriteLine();
        }

        public static int GetCoordinateFromConsole(string purpose, char measure)
        {
            Console.WriteLine();
            Console.WriteLine($"{purpose}");

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
