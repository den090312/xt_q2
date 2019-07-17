
using _2._1.ROUND;
using System;

namespace _2._8.GAME
{
    public class Bonus : Subject
    {
        public Type BonusType { get; private set; }

        public enum Type
        {
            Apple = 0,
            Cherry = 1,
            Limon = 2
        }

        public Bonus(Point userCenter, Type userType)
        {
            Shape = new Circle(userCenter, radius);
            BonusType = userType;
        }
    }
}
