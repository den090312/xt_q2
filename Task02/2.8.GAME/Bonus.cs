
using _2._1.ROUND;
using System;

namespace _2._8.GAME
{
    public class Bonus : Subject
    {
        private Circle shape = new Circle(new Point(0, 0), 1);

        public Type type;

        public Circle Shape { get => shape; private set => shape = value; }

        public enum Type
        {
            Apple = 0,
            Cherry = 1
        }

        public Bonus(Circle Shape, Type Type)
        {
            shape = Shape;
            type = Type;
        }
    }
}
