using System;

namespace _2._1.ROUND
{
    public class Round : Circle
    {
        public Round(Point userCenterCoordinates, double userRadius) : base(userCenterCoordinates, userRadius)
        {
        }

        public double Area => Math.PI * Radius * Radius;


    }
}
