
using _2._1.ROUND;

namespace _2._8.GAME
{
    public abstract class Subject
    {
        private Point center = new Point(0, 0);
        protected readonly double radius = 3;
        protected Circle shape = new Circle(new Point(0, 0), 3);
    }
}
