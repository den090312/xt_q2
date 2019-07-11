using System;

namespace _2._1.ROUND
{
    class Program
    {
        static void Main(string[] args)
        {
            //инициализируем координаты точки
            var myCoordinates = new Point(5, 20);

            //инициализируем радиус
            double myRadius = 20;

            //создаем объект класса "Круг"
            var myRound = new Round(myCoordinates, myRadius);

            WriteRoundInfo(myRound);

            //изменяем координаты         
            myRound.СenterCoordinates = new Point(10, 10);

            //изменяем радиус
            myRound.Radius = 50;

            WriteRoundInfo(myRound);

            //изменяем координату X
            myRound.СenterCoordinates.X = -15;

            WriteRoundInfo(myRound);
        }

        private static void WriteRoundInfo(Round myRound)
        {
            Console.WriteLine($"Координаты центра круга: ({myRound.СenterCoordinates.X},{myRound.СenterCoordinates.Y})");
            Console.WriteLine($"Радиус круга: {myRound.Radius}");
            Console.WriteLine($"Длина окружности: {myRound.Circumference}");
            Console.WriteLine($"Площадь круга: {myRound.Area}");
            Console.WriteLine();
        }
    }
}
