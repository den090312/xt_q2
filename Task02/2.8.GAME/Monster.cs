using _2._1.ROUND;
using System;

namespace _2._8.GAME
{
    public class Monster : Subject, IMovable
    {
        public Type type;

        public enum Type
        {
            Bear = 0,
            Wolf = 1,
            Snake = 2
        }

        public Monster(Point userCenter, Type type)
        {
            Shape = new Circle(userCenter, radius);
            this.type = type;
        }

        public void Move()
        {
            throw new NotImplementedException();
        }
    }
}
