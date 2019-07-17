using _2._1.ROUND;
using System;

namespace _2._8.GAME
{
    public class Monster : Subject, IMovable
    {
        public enum Type
        {
            Bear = 0,
            Wolf = 1,
            Snake = 2
        }

        public Type MonsterType { get; private set; }

        public Monster(Point userCenter, Type userType)
        {
            Shape = new Circle(userCenter, radius);
            MonsterType = userType;
        }

        public void Move()
        {
            throw new NotImplementedException();
        }
    }
}
