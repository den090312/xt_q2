using System;

namespace _2._1.ROUND
{
    public class Circle
    {
        private Point edgeCoordinates = new Point(10, 10);
        private Point centerCoordinates = new Point(0, 0);

        public Circle(Point userCenterCoordinates, Point userEdgeCoordinates)
        {
            CheckCoordinates(userCenterCoordinates, userEdgeCoordinates);
            CenterCoordinates = userCenterCoordinates;
            EdgeCoordinates = userEdgeCoordinates;
        }

        public Point CenterCoordinates
        {
            get => centerCoordinates;
            set
            {
                CheckCoordinates(centerCoordinates, edgeCoordinates);
                centerCoordinates = value;
            }
        }

        public Point EdgeCoordinates
        {
            get => edgeCoordinates;
            set
            {
                CheckCoordinates(centerCoordinates, edgeCoordinates);
                edgeCoordinates = value;
            }
        }

        public double Radius => Math.Sqrt
        ( 
            Math.Pow(edgeCoordinates.X - centerCoordinates.X, 2) +
            Math.Pow(edgeCoordinates.Y - centerCoordinates.Y, 2)
        );

        public double Circumference => 2 * Math.PI * Radius;

        private void CheckCoordinates(Point userCenterCoordinates, Point userEdgeCoordinates)
        {
            if (userEdgeCoordinates == userCenterCoordinates)
            {
                throw new Exception("Координаты края окружности не могут быть равны координатам центра!");
            }
        }
    }
}
