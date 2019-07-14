using System;

namespace _2._1.ROUND
{
    public class Circle
    {
        private Point edgeCoordinates;
        private Point centerCoordinates = new Point(0, 0);

        public Point CenterCoordinates
        {
            get => centerCoordinates;
            set
            {
                CheckCoordinates();
                centerCoordinates = value;
            }
        }

        public Point EdgeCoordinates
        {
            get => edgeCoordinates;
            set
            {
                CheckCoordinates();
                edgeCoordinates = value;
            }
        }

        public Radius CircleRadius { get; set; }
        public double Circumference => 2 * Math.PI * CircleRadius.Value;

        public Circle(Point userCenterCoordinates, Radius userRadius)
        {
            CenterCoordinates = userCenterCoordinates;
            CircleRadius = userRadius;
        }

        private void CheckCoordinates()
        {
            if (EdgeCoordinates < CenterCoordinates)
            {
                throw new Exception("Координаты края окружности должны быть меньше координат центра!");
            }

            if (EdgeCoordinates == CenterCoordinates)
            {
                throw new Exception("Координаты края окружности не могут быть равны координатам центра!");
            }
        }
    }
}
