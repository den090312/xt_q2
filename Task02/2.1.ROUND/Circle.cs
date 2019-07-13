using System;

namespace _2._1.ROUND
{
    public class Circle
    {
        public Point СenterCoordinates = new Point(0, 0);
        public Radius CircleRadius { get; set; }
        public double Circumference => 2 * Math.PI * CircleRadius.Value;
        public Circle(Point userCenterCoordinates, Radius userRadius)
        {
            СenterCoordinates = userCenterCoordinates;
            CircleRadius = userRadius;
        }
    }
}
