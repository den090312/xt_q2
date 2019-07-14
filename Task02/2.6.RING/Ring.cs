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

        public Ring(Circle userCircle, Point userEdge)
        {
            var userRingRadius = GetRingRadius(userCircle, userEdge);
            RadiusCheck(userCircle.Radius, userRingRadius);

            radius = userRingRadius;
            Circle = userCircle;
            Edge = userEdge;
        }

        public Ring(Circle userCircle, double userOuterRadius)
        {
            innerRadius = userCircle.Radius;
            outerRadius = userOuterRadius 
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
                RadiusCheck(Circle.Radius, value);
                radius = value;
            }
        }

        public Point Edge { get; set; }

        public Circle Circle { get; set; }

        public double Area => Circle.Radius > Radius
                    ? Math.PI * (Math.Pow(Circle.Radius, 2) - Math.Pow(Radius, 2))
                    : Math.PI * (Math.Pow(Radius, 2) - Math.Pow(Circle.Radius, 2));

        public double TotalCircumference => 2 * Math.PI * Radius + 2 * Math.PI * Circle.Radius;

        private void RadiusCheck(double userCircleRadius, double userRadius)
        {
            if (userCircleRadius == userRadius)
            {
                throw new Exception("Окружности кольца не должны совпадать!");
            }

            if (userCircleRadius < userRadius)
            {
                throw new Exception("Внешний радиус должен быть больше внутреннего!");
            }
        }
    }
}
