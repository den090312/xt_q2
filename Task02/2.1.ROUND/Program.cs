using System;

namespace _2._1.ROUND
{
    class Program
    {
        private static void Main(string[] args)
        {
            //инициализируем координаты точки
            var myCoordinates = new Point(5, 20);

            //радиус
            Console.WriteLine("Радиус круга");
            bool itIsRadius = true;
            double myRadius = GetIntFromConsole(itIsRadius);
            Console.WriteLine();

            //создаем объект класса "Круг"
            var myRound = new Round(myCoordinates, myRadius);

            //задаем координаты 
            itIsRadius = false;
            Console.WriteLine("Центр круга. Координата X");
            int X = GetIntFromConsole(itIsRadius);
            Console.WriteLine("Центр круга. Координата Y");
            int Y = GetIntFromConsole(itIsRadius);

            myRound.СenterCoordinates = new Point(X, Y);

            WriteRoundInfo(myRound);
        }

        private static void WriteRoundInfo(Round myRound)
        {
            Console.WriteLine();
            Console.WriteLine($"Координаты центра круга: ({myRound.СenterCoordinates.X},{myRound.СenterCoordinates.Y})");
            Console.WriteLine($"Радиус круга: {myRound.Radius}");
            Console.WriteLine($"Длина окружности: {myRound.Circumference}");
            Console.WriteLine($"Площадь круга: {myRound.Area}");
            Console.WriteLine();
        }

        private static int GetIntFromConsole(bool itIsRadius)
        {
            int intFromConsole;

            bool isInt;

            if (itIsRadius)
            {
                do
                {
                    Console.WriteLine($"Введите целое положительное число меньше или равно {int.MaxValue}:");
                    isInt = int.TryParse(Console.ReadLine(), out intFromConsole);

                    if (isInt)
                    {
                        isInt = intFromConsole > 0;
                    }
                }
                while (isInt == false);
            }
            else
            {
                do
                {
                    Console.WriteLine($"Введите целое число меньше или равно {int.MaxValue}:");
                    isInt = int.TryParse(Console.ReadLine(), out intFromConsole);

                    if (isInt)
                    {
                        isInt = true;
                    }
                }
                while (isInt == false);
            }

            return intFromConsole;
        }
    }
}
