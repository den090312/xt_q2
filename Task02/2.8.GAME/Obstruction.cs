using _2._1.ROUND;

namespace _2._8.GAME
{
    public class Obstruction : Subject
    {
        public Type BonusType { get; private set; }

        public enum Type
        {
            Stone = 0,
            Tree = 1
        }

        public Obstruction(Point userCenter, Type userType)
        {
            Shape = new Circle(userCenter, radius);
            BonusType = userType;
        }
    }
}

