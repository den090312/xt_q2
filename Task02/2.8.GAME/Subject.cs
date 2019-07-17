
using _2._1.ROUND;

namespace _2._8.GAME
{
    public abstract class Subject
    {
        private Circle shape = new Circle(new Point(0, 0), 3);

        public virtual Circle Shape { get => shape; set => shape = value; }
    }
}
