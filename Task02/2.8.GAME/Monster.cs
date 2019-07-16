using _2._1.ROUND;
using System;

namespace _2._8.GAME
{
    public class Monster : Subject, IMovable
    {
        private Circle shape = new Circle(new Point(0, 0), 1);

        public Type type;

        public Circle Shape { get => shape; private set => shape = value; }

        public enum Type
        {
            Bear = 0,
            Wolf = 1,
            Snake = 2
        }

        public Monster(Circle shape, Type type)
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
