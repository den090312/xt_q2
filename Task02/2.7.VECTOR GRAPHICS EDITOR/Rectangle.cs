using _2._1.ROUND;

namespace _2._7.VECTOR_GRAPHICS_EDITOR
{
    public class Rectangle
    {
        private Line line2;

        public Line Line1 { get; set; }
        public Line Line2
        {
            get => line2;
            private set
            {
                line2 = GetSecondLine(value);
            }
        }

        public Rectangle(Line userLine)
        {
            Line1 = userLine;
        }

        private Line GetSecondLine(Line firstLine) => new Line
        (
            new Point(firstLine.Point1.X, firstLine.Point2.Y), 
            new Point(firstLine.Point2.X, firstLine.Point1.Y)
        );
    }
}
