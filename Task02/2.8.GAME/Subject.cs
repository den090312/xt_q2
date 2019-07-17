
using _2._1.ROUND;

namespace _2._8.GAME
{
    public abstract class Subject
    {
        public Point center = new Point(0, 0);
        public readonly double radius = 3;
        public Circle shape = new Circle(new Point(0, 0), 3);
    }
}
