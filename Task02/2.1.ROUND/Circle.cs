using System;

namespace _2._1.ROUND
{
    public class Circle
    {
        private Point centerCoordinates = new Point(0, 0);
        private double radius = 0;

        public Point EdgeCoordinates { get; private set; } = new Point(0, 1);

        //конструктор через точки
        public Circle(Point userCenterCoordinates, Point userEdgeCoordinates)
        {
            PointNullCheck(userCenterCoordinates);
            PointNullCheck(userEdgeCoordinates);

            CheckCoordinates(userCenterCoordinates, userEdgeCoordinates);
            CenterCoordinates = userCenterCoordinates;
            EdgeCoordinates = userEdgeCoordinates;
        }

        //конструктор с указанием радиуса
        public Circle(Point userCenterCoordinates, double userRadius)
        {
            PointNullCheck(userCenterCoordinates);

            if (userRadius <= 0)
            {
                throw new Exception("Радиус должен быть больше нуля!");
            }

            centerCoordinates = userCenterCoordinates;
            radius = userRadius;
            EdgeCoordinates = null;
        }

        public Point CenterCoordinates
        {
            get => centerCoordinates;
            private set
            {
                CheckCoordinates(centerCoordinates, EdgeCoordinates);
                centerCoordinates = value;
            }
        }

        public double Radius
        {
            get
            {
                if (radius == 0)
                {
                    return Math.Sqrt
                    (
                        Math.Pow(EdgeCoordinates.X - centerCoordinates.X, 2) +
                        Math.Pow(EdgeCoordinates.Y - centerCoordinates.Y, 2)
                    );
                }
                else
                {
                    return radius;
                }
            }
            private set => radius = value;
        }

        public double Circumference => 2 * Math.PI * Radius;

        private void CheckCoordinates(Point userCenterCoordinates, Point userEdgeCoordinates)
        {
            if (userEdgeCoordinates == userCenterCoordinates)
            {
                throw new Exception("Координаты края окружности не могут быть равны координатам центра!");
            }
        }

        public static bool operator ==(Circle circle1, Circle circle2)
        {

            return circle1.centerCoordinates == circle2.centerCoordinates 
                && circle1.EdgeCoordinates == circle2.EdgeCoordinates 
                && circle1.radius == circle2.radius ? true : false;
        }

        public static bool operator !=(Circle circle1, Circle circle2) => circle1.centerCoordinates != circle2.centerCoordinates ? true : false;

        public override bool Equals(object obj)
        {
            return obj is Circle circle 
                && centerCoordinates == circle.centerCoordinates 
                && EdgeCoordinates == circle.EdgeCoordinates 
                && radius == circle.radius;
        }

        public override int GetHashCode() => centerCoordinates.GetHashCode() ^ EdgeCoordinates.GetHashCode() ^ radius.GetHashCode();

        private void PointNullCheck(Point point)
        {
            if (point is null)
            {
                throw new ArgumentNullException($"{nameof(point)} is null!");
            }
        }
    }
}
