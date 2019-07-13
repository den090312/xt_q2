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
                    throw new Exception("Радиусы кольца не могут быть равны!");
                }

                ringRadius = value;
            }
        }

        public double RingCircumference => 2 * Math.PI * RingRadius;
        public double TotalCircumference => Circumference + RingCircumference;
    }
}
