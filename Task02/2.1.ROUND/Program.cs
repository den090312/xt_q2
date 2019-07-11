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

        static void WriteRoundInfo(Round myRound)
        {
            Console.WriteLine($"Координаты центра круга: ({myRound.СenterCoordinates.X},{myRound.СenterCoordinates.Y})");
            Console.WriteLine($"Радиус круга: {myRound.Radius}");
            Console.WriteLine($"Длина окружности: {myRound.Circumference}");
            Console.WriteLine($"Площадь круга: {myRound.Area}");
            Console.WriteLine();
        }

        //класс "Точка" с двумя координатами
        public class Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Point(int X, int Y)
            {
                this.X = X;
                this.Y = Y;
            }
        }

        //класс "Круг"
        public class Round
        {
            //инициализируем координаты центра круга
            public Point СenterCoordinates = new Point(0, 0);

            //радиус круга            
            private double radius;

            //длина окружности
            public double Circumference => 2 * Math.PI * Radius;

            //площадь круга
            public double Area => Math.PI * Radius * Radius;

            public double Radius
            {
                get => radius;
                set
                {
                    if (radius <= 0)
                    {
                        throw new ArgumentException("Отрицательные значения и ноль недопустимы!");
                    }
                    else
                    {
                        radius = value;
                    }
                }
            }

            //конструктор
            public Round(Point userCenterCoordinates, double userRadius)
            {
                if (userRadius <= 0)
                {
                    throw new ArgumentException("Отрицательные значения и ноль недопустимы!");
                }
                else
                {
                    СenterCoordinates = userCenterCoordinates;
                    radius = userRadius;
                }
            }
        }
    }
}
