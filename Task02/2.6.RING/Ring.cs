using _2._1.ROUND;
using System;

namespace _2._6.RING
{
    class Ring : Circle
    {
        private double ringRadius;

        public double RingRadius
        {
            get => ringRadius;
            set
            {
                CheckRadiusAboveZero(value);

                if (value == Radius)
                {
                    throw new Exception("Радиус кольца не может быть равен радиусу окружности!");
                }

                ringRadius = value;
            }
        }

        public double RingArea => RingRadius > Radius
                    ? Math.PI * (RingRadius * RingRadius - Radius * Radius)
                    : Math.PI * (Radius * Radius - RingRadius * RingRadius);

        public double RingCircumference => 2 * Math.PI * RingRadius;
        public double TotalCircumference => Circumference + RingCircumference;

        public Ring(Point userCenterCoordinates, double userRadius) : base(userCenterCoordinates, userRadius)
        {
        }
    }
}
