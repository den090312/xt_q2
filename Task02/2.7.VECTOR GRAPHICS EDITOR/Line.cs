using _2._1.ROUND;
using System;

namespace _2._7.VECTOR_GRAPHICS_EDITOR
{
    public class Line
    {
        public Line(Point userPoint1, Point userPoint2)
        {
            PointsCheck(userPoint1, userPoint2);

            Point1 = userPoint1;
            Point2 = userPoint2;
        }

        public Point Point1 { get; set; }

        public Point Point2 { get; set; }

        private static void PointsCheck(Point userPoint1, Point userPoint2)
        {
            if (userPoint1 == userPoint2)
            {
                throw new ArgumentException("Линия не может состоять из двух одинаковых точек!");
            }
        }
    }
}
