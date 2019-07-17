using _2._1.ROUND;

namespace _2._8.GAME
{
    public class Obstruction : Subject
    {
        public Type type;

        public enum Type
        {
            Stone = 0,
            Tree = 1,
            Pit = 2
        }

        public Obstruction(Point userCenter, Type type)
        {
            Shape = new Circle(userCenter, radius);
            this.type = type;
        }
    }
}

