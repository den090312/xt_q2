using _2._1.ROUND;
using System;

namespace _2._8.GAME
{
    public class Player : Subject, IControllable
    {
        private int health = 6;
        private Direction direction;
        private Obstruction obstruction;

        public Player(Point userCenter)
        {
            shape = new Circle(userCenter, radius);
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        public Direction GetDirection() => direction;

        public void GoLeft(int countSteps)
        {
            direction = Direction.Left;
            shape.MoveTo(new Point(shape.CenterCoordinates.X - countSteps, shape.CenterCoordinates.Y));
        }

        public void GoRight(int countSteps)
        {
            direction = Direction.Right;
            shape.MoveTo(new Point(shape.CenterCoordinates.X + countSteps, shape.CenterCoordinates.Y));
        }

        public void GoUp(int countSteps)
        {
            direction = Direction.Up;
            shape.MoveTo(new Point(shape.CenterCoordinates.X, shape.CenterCoordinates.Y + countSteps));
        }

        public void GoDown(int countSteps)
        {
            direction = Direction.Down;
            shape.MoveTo(new Point(shape.CenterCoordinates.X, shape.CenterCoordinates.Y - countSteps));
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
