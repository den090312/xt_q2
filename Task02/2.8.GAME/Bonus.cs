
using _2._1.ROUND;
using System;

namespace _2._8.GAME
{
    public class Bonus : Subject
    {
        private Circle shape;
        private Type type;

        private enum Type
        {
            Apple = 0,
            Cherry = 1
        }

        private Bonus(Circle Shape, Type Type)
        {
            shape = Shape;
            type = Type;
        }
    }
}
