﻿using _2._1.ROUND;

namespace _2._7.VECTOR_GRAPHICS_EDITOR
{
    class TwoPointsFigure
    {
        public Point Point1 { get; set; }
        public Point Point2 { get; set; }

        public TwoPointsFigure(Point userPoint1, Point userPoint2)
        {
            Point1 = userPoint1;
            Point2 = userPoint2;
        }

    }
}