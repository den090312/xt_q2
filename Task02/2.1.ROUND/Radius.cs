using System;

namespace _2._1.ROUND
{
    public class Radius
    {
        private double value;

        public double Value
        {
            get => value;
            set
            {
                CheckRadiusAboveZero(value);
                this.value = value;
            }
        }

        public Radius(double radiusValue)
        {
            value = radiusValue;
        }

        private static void CheckRadiusAboveZero(double radius)
        {
            if (radius <= 0)
            {
                throw new ArgumentException("Отрицательное значение радиуса недопустимо!");
            }
        }
    }
}
