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
            Wolf = 1,
            Snake = 2
        }

        private Monster(Circle shape, Type type)
        {
            this.shape = shape;
            this.type = type;
        }

        public void Move()
        {
            throw new NotImplementedException();
        }
    }
}
