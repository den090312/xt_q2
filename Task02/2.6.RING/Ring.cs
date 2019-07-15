using _2._1.ROUND;
using System;

namespace _2._6.RING
{
    public class Ring
    {
        //для создания кольца через внешний-внутренний радиусы
        private readonly double outerRadius;
        private readonly double innerRadius;

        //для создания кольца через точки
        private double radius;

        //конструктор через кольцо и край внешней окружности
        public Ring(Circle userCircle, Point userEdge)
        {
            var userOuterRingRadius = GetRingRadius(userCircle, userEdge);
            RingRadiusCheck(userCircle.Radius, userOuterRingRadius);

            radius = userOuterRingRadius;
            Circle = userCircle;
            Edge = userEdge;
        }

        //конструктор через кольцо и внешний радиус
        public Ring(Circle userCircle, double userOuterRingRadius)
        {
            RingRadiusCheck(userCircle.Radius, userOuterRingRadius);

            innerRadius = userCircle.Radius;
            outerRadius = userOuterRingRadius;
            Circle = userCircle;
        }

        public double GetRingRadius(Circle Circle, Point Edge) => Math.Sqrt
        (
            Math.Pow(Edge.X - Circle.CenterCoordinates.X, 2) + 
            Math.Pow(Edge.Y - Circle.CenterCoordinates.Y, 2)
        );

        public double Radius
        {
            get => outerRadius == 0 ? radius : outerRadius;
            set
            {
                RingRadiusCheck(Circle.Radius, value);
                radius = value;
            }
        }

        public Point Edge { get; set; }

        public Circle Circle { get; set; }

        public double Area => Circle.Radius > Radius
                    ? Math.PI * (Math.Pow(Circle.Radius, 2) - Math.Pow(Radius, 2))
                    : Math.PI * (Math.Pow(Radius, 2) - Math.Pow(Circle.Radius, 2));

        public double TotalCircumference => 2 * Math.PI * Radius + 2 * Math.PI * Circle.Radius;

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
    }
}
