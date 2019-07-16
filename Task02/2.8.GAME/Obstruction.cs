using _2._1.ROUND;

namespace _2._8.GAME
{
    public class Obstruction : Subject
    {
        private Circle shape;
        private Type type;

        private enum Type
        {
            Stone = 0,
            Tree = 1,
            Pit = 2
        }

        private Obstruction(Circle shape, Type type)
        {
            this.shape = shape;
            this.type = type;
        }
    }
}

