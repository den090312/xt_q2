using _2._1.ROUND;

namespace _2._8.GAME
{
    public class Obstruction : Subject
    {
        public enum Type
        {
            Stone = 0,
            Tree = 1
        }

        public Type ObstructionType { get; private set; }

        public Obstruction(Point userCenter, Type userType)
        {
            Shape = new Circle(userCenter, radius);
            ObstructionType = userType;
        }
    }
}

