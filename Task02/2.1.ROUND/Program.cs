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
            Console.WriteLine("Введите радиус круга:");
            bool itIsRadius = true;
            double myRadius = GetIntFromConsole(itIsRadius);

            //создаем объект класса "Круг"
            var myRound = new Round(myCoordinates, myRadius);

            WriteRoundInfo(myRound);

            //изменяем координаты   
            itIsRadius = false;
            myRound.СenterCoordinates = new Point(GetIntFromConsole(itIsRadius), GetIntFromConsole(itIsRadius));

            //изменяем радиус
            myRound.Radius = 50;

            WriteRoundInfo(myRound);

            //изменяем координату X
            myRound.СenterCoordinates.X = -15;

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

        static int GetIntFromConsole(bool itIsRadius)
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
