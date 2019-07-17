using _2._1.ROUND;
using System;

namespace _2._8.GAME
{
    public class Player : Subject, IControllable
    {
        private int health = 6;
        private Direction direction;

        public enum Direction
        {
            Up = 0,
            Down = 1,
            Left = 2,
            Right = 3
        }

        public Player(Point userCenter)
        {
            shape = new Circle(userCenter, radius);
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        public Direction GetDirection() => direction;

        private Direction SetDirection(Direction playerDirection) => direction = playerDirection;

        public void GoLeft(int countSteps)
        {
            direction = Direction.Left;
        }

        public void GoRight(int countSteps)
        {
            direction = Direction.К;
        }

        public void GoUp(int countSteps)
        {
            direction = Direction.Up;
        }

        public void GoDown(int countSteps)
        {
            direction = Direction.Down;
        }

        public void GetHealth(int amountHealth)
        {
            if (health != 10)
            {
                health += amountHealth;
            }
        }
    }
}
