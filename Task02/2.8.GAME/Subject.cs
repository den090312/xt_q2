
using _2._1.ROUND;

namespace _2._8.GAME
{
    public abstract class Subject
    {
        public Point Location { get; protected set; } = new Point(0, 0);

        protected static bool IsOverlayed(Subject sub1, Subject sub2) => sub1.Location == sub2.Location;
    }
}
