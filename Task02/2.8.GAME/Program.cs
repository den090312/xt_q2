using System;

namespace _2._8.GAME
{
    public class Program
    {
        private static void Main(string[] args)
        {

        }

        public class Player : Subject , IMovable
        {
            private int health;
            private int speed;

            public void Move()
            {
                throw new NotImplementedException();
            }
        }

        public class Bonus : Subject
        {

        }

        public class Monster : Subject, IMovable
        {
            public void Move()
            {
                throw new NotImplementedException();
            }
        }

        public class Obstruction : Subject
        {

        }
    }
}
