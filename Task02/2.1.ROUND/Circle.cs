using System;

namespace _2._1.ROUND
{
    public class Circle
    {
        public Point СenterCoordinates = new Point(0, 0);
        public Radius CircleRadius { get; set; }

        //длина окружности
        public double Circumference => 2 * Math.PI * CircleRadius.Value;

        //конструктор
        public Circle(Point userCenterCoordinates, Radius userRadius)
        {
            СenterCoordinates = userCenterCoordinates;
            CircleRadius = userRadius;
        }
    }
}
