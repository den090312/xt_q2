using System;

namespace _2._1.ROUND
{
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
                    throw new ArgumentException("Отрицательное значение радиуса недопустимо!");
                }

                radius = value;
            }
        }

        //конструктор
        public Round(Point userCenterCoordinates, double userRadius)
        {
            if (userRadius <= 0)
            {
                throw new ArgumentException("Отрицательное значение радиуса недопустимо!");
            }

            СenterCoordinates = userCenterCoordinates;
            radius = userRadius;
        }
    }
}
