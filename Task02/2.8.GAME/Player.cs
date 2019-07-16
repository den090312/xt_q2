using _2._1.ROUND;
using System;

namespace _2._8.GAME
{
    public class Player : Subject, IControllable
    {
        private Circle shape;
        private int health = 10;

        private Player(Circle playerShape)
        {
            shape = playerShape;
        }

        public void GoLeft()
        {
            throw new NotImplementedException();
        }

        public void GoRight()
        {
            throw new NotImplementedException();
        }

        public void GoUp()
        {
            throw new NotImplementedException();
        }
    }
}
