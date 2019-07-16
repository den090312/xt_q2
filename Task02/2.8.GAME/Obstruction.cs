using _2._1.ROUND;

namespace _2._8.GAME
{
    public class Obstruction : Subject
    {
        private Circle shape = new Circle(new Point(0, 0), 1);

        public Type type;

        public Circle Shape { get => shape; private set => shape = value; }

        public enum Type
        {
            Stone = 0,
            Tree = 1,
            Pit = 2
        }

        public Obstruction(Circle shape, Type type)
        {
            this.shape = shape;
            this.type = type;
        }
    }
}

