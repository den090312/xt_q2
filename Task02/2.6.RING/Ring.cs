using _2._1.ROUND;
using System;

namespace _2._6.RING
{
    public class Ring
    {
        //для создания кольца через внешний-внутренний радиусы
        private double outerRadius;
        private double innerRadius;

        //для создания кольца через точки
        private double radius;

        public readonly Point edge = new Point(6, 6);

        public Circle Circle { get; private set; } = new Circle(new Point(0, 0), new Point(3, 3));

        public double Radius
        {
            get => outerRadius == 0 ? radius : outerRadius;
            private set
            {
                RingRadiusCheck(Circle.Radius, value);
                radius = value;
            }
        }

        public double Area => Circle.Radius > Radius
                    ? Math.PI * (Math.Pow(Circle.Radius, 2) - Math.Pow(Radius, 2))
                    : Math.PI * (Math.Pow(Radius, 2) - Math.Pow(Circle.Radius, 2));

        public double TotalCircumference => 2 * Math.PI * Radius + 2 * Math.PI * Circle.Radius;

        //конструктор через кольцо и край внешней окружности
        public Ring(Circle userCircle, Point userEdge)
        {
            CircleNullCheck(userCircle);
            PointNullCheck(userEdge);

            var userOuterRingRadius = GetRingRadius(userCircle, userEdge);
            RingRadiusCheck(userCircle.Radius, userOuterRingRadius);

            radius = userOuterRingRadius;
            Circle = userCircle;
            edge = userEdge;
        }

        //конструктор через кольцо и внешний радиус
        public Ring(Circle userCircle, double userOuterRingRadius)
        {
            CircleNullCheck(userCircle);
            RingRadiusCheck(userCircle.Radius, userOuterRingRadius);

            innerRadius = userCircle.Radius;
            outerRadius = userOuterRingRadius;
            Circle = userCircle;
        }

        private double GetRingRadius(Circle circle, Point edge) => Math.Sqrt
        (
            Math.Pow(edge.X - circle.CenterCoordinates.X, 2) +
            Math.Pow(edge.Y - circle.CenterCoordinates.Y, 2)
        );

        private void RingRadiusCheck(double userCircleInnerRadius, double userOuterRingRadius)
        {
            if (userOuterRingRadius < 0)
            {
                throw new Exception("Внешний радиус не может быть отрицательным!");
            }

            if (userCircleInnerRadius == userOuterRingRadius)
            {
                throw new Exception("Окружности кольца не должны совпадать!");
            }

            if (userOuterRingRadius < userCircleInnerRadius)
            {
                throw new Exception("Внешний радиус должен быть больше внутреннего!");
            }
        }

        private static void CircleNullCheck(Circle circle)
        {
            if (circle is null)
            {
                throw new ArgumentNullException($"{nameof(circle)} is null!");
            }
        }

        private static void PointNullCheck(Point point)
        {
            if (point is null)
            {
                throw new ArgumentNullException($"{nameof(point)} is null!");
            }
        }
    }
}
