
using _2._1.ROUND;

namespace _2._8.GAME
{
    public abstract class Subject
    {
        protected readonly double radius = 3;

        public Circle Shape { get; protected set; } = new Circle(new Point(0, 0), 3);

        protected static bool IsOverlayed(Subject sub1, Subject sub2) => sub1.Shape.CenterCoordinates == sub2.Shape.CenterCoordinates;
    }
}
