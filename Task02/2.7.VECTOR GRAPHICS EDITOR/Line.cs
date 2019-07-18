using _2._1.ROUND;
using System;

namespace _2._7.VECTOR_GRAPHICS_EDITOR
{
    public class Line
    {
        public Point Point1 { get; private set; } = new Point(0, 0);

        public Point Point2 { get; private set; } = new Point(0, 0);

        public Line(Point userPoint1, Point userPoint2)
        {
            NullCheck(userPoint1);
            NullCheck(userPoint2);

            PointsCheck(userPoint1, userPoint2);

            Point1 = userPoint1;
            Point2 = userPoint2;
        }

        private static void PointsCheck(Point userPoint1, Point userPoint2)
        {
            NullCheck(userPoint1);
            NullCheck(userPoint2);

            if (userPoint1 == userPoint2)
            {
                throw new ArgumentException("Линия не может состоять из двух одинаковых точек!");
            }
        }

        private static void NullCheck(Point point)
        {
            if (point is null)
            {
                throw new ArgumentNullException($"{nameof(point)} is null!");
            }
        }
    }
}
