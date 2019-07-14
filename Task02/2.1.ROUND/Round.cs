using System;

namespace _2._1.ROUND
{
    public class Round : Circle
    {
        public Round(Point userCenterCoordinates, Point userEdgeCoordinates) : base(userCenterCoordinates, userEdgeCoordinates)
        {
        }

        public double Area => Math.PI * Radius * Radius;
    }
}
