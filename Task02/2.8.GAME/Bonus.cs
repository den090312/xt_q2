
using _2._1.ROUND;
using System;

namespace _2._8.GAME
{
    public class Bonus : Subject
    {
        public Type type;

        public enum Type
        {
            Apple = 0,
            Cherry = 1,
            Limon = 2
        }

        public Bonus(Point userCenter, Type type)
        {
            shape = new Circle(userCenter, radius);
            this.type = type;
        }
    }
}
