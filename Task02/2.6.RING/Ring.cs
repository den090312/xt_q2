using _2._1.ROUND;
using System;

namespace _2._6.RING
{
    public class Ring
    {
        private Circle outerCircle;
        private Circle innerCircle;

        public Point СenterCoordinates = new Point(0, 0);
        public Circle OuterCircle
        {
            get => outerCircle;
            set
            {
                CheckRing();
                outerCircle = value;
            }
        }

        public Circle InnerCircle
        {
            get => innerCircle;
            set
            {
                CheckRing();
                innerCircle = value;
            }
        }

        public double Area => Math.PI * (Math.Pow(OuterCircle.Radius, 2) - Math.Pow(InnerCircle.Radius, 2));
        public double TotalCircumference => 2 * Math.PI * InnerCircle.Radius + 2 * Math.PI * OuterCircle.Radius;

        public Ring(Circle userOuterCircle, Circle userInnerCircle)
        {
            CheckRing();
            OuterCircle = userOuterCircle;
            InnerCircle = userInnerCircle;
        }

        private void CheckRing()
        {
            if (OuterCircle.Radius < InnerCircle.Radius)
            {
                throw new Exception("Внешнее кольцо не может совпадать с внутренним!");
            }

            if (OuterCircle.CenterCoordinates < InnerCircle.CenterCoordinates)
            {
                throw new Exception("Внешнее кольцо должно быть больше внутреннего!");
            }

            if (OuterCircle.CenterCoordinates != InnerCircle.CenterCoordinates)
            {
                throw new Exception("Центры двух окружностей должны совпадать!");
            }
        }
    }
}
