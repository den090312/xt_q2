using _2._1.ROUND;
using System;

namespace _2._8.GAME
{
    public class Monster : Subject, IMovable
    {
        private Circle shape;
        private Type type;

        private enum Type
        {
            Bear = 0,
            Wolf = 1
        }

        private Monster(Circle Shape, Type Type)
        {
            shape = Shape;
            type = Type;
        }

        public void GoUp()
        {
            throw new NotImplementedException();
        }
    }
}
