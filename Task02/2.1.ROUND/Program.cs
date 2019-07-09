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

            //выводим радиус круга
            Console.WriteLine($"Радиус круга: {myRound.Radius}");

            //проверяем длину окружности
            Console.WriteLine($"Длина окружности: {myRound.Circumference}");

            //проверяем площадь круга
            Console.WriteLine($"Площадь круга: {myRound.Square}");
        }

        //структура "Точка" с двумя координатами
        public struct Point
        {
            public int x, y;
            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        //класс "Круг"
        public class Round
        {
            //координаты центра круга
            public Point centerCoordinates;

            //радиус круга
            private double radius;

            public Round(Point userCenterCoordinates, double userRadius)
            {
                centerCoordinates = userCenterCoordinates;
                radius = userRadius;
            }

            public double Radius
            {
                get => radius;

                set
                {
                    if (value <= 0)
                    {
                        throw new Exception("Отрицательные значения и ноль недопустимы!");
                    }
                    else
                    {
                        radius = value;
                    }
                }
            }

            //длина окружности
            public double Circumference => 2 * Math.PI * radius;

            //площадь круга
            public double Square => Math.PI * radius * radius;
        }
    }
}
