
using _2._1.ROUND;

namespace _2._8.GAME
{
    public abstract class Subject
    {
        protected readonly double radius = 3;

        public Circle Shape { get; protected set; } = new Circle(new Point(0, 0), 3);
    }
}
