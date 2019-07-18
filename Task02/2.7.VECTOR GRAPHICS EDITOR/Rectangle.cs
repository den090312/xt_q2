using _2._1.ROUND;
using System;

namespace _2._7.VECTOR_GRAPHICS_EDITOR
{
    public class Rectangle
    {
        public Line Line1 { get; set; }

        public Line Line2 => GetSecondLine(Line1);

        public Rectangle(Line userLine)
        {
            NullCheck(userLine);

            Line1 = userLine;
        }

        private Line GetSecondLine(Line firstLine) => new Line
        (
            new Point(firstLine.Point1.X, firstLine.Point2.Y),
            new Point(firstLine.Point2.X, firstLine.Point1.Y)
        );

        private static void NullCheck(Line line)
        {
            if (line is null)
            {
                throw new ArgumentNullException($"{nameof(line)} is null!");
            }
        }
    }
}
