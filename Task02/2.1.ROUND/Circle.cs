using System;

namespace _2._1.ROUND
{
    public class Circle
    {   
        private double radius;

        public Point СenterCoordinates = new Point(0, 0);

        //длина окружности
        public double Circumference => 2 * Math.PI * Radius;

        public double Radius
        {
            get => radius;
            set
            {
                CheckRadiusAboveZero(value);
                radius = value;
            }
        }

        //конструктор
        public Circle(Point userCenterCoordinates, double userRadius)
        {
            CheckRadiusAboveZero(userRadius);
            СenterCoordinates = userCenterCoordinates;
            radius = userRadius;
        }

        protected static void CheckRadiusAboveZero(double radius)
        {
            if (radius <= 0)
            {
                throw new ArgumentException("Отрицательное значение радиуса недопустимо!");
            }
        }
    }
}
