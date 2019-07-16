
using _2._1.ROUND;
using System;

namespace _2._8.GAME
{
    public class Bonus : Subject
    {
        public Type type;

        public override Circle Shape { get; set; }

        public enum Type
        {
            Apple = 0,
            Cherry = 1
        }

        public Bonus(Circle shape, Type type)
        {
            Shape = shape;
            this.type = type;
        }
    }
}
