using _2._1.ROUND;
using System;

namespace _2._8.GAME
{
    public class Player : Subject, IControllable
    {
        private int health = 6;
        private bool lefttWall = false;
        private bool rightWall = false;
        private bool upWall = false;
        private bool downWall = false;
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
            SetDirection(Direction.Left);

            if (!lefttWall)
            {
                shape.MoveTo(new Point(shape.CenterCoordinates.X - countSteps, shape.CenterCoordinates.Y));
                rightWall = false;
                upWall = false;
                downWall = false;
            }

            direction = Direction.Left;
        }

        public void GoRight(int countSteps)
        {
            SetDirection(Direction.Right);

            if (!rightWall)
            {
                shape.MoveTo(new Point(shape.CenterCoordinates.X + countSteps, shape.CenterCoordinates.Y));
                lefttWall = false;
                upWall = false;
                downWall = false;
            }
        }

        public void GoUp(int countSteps)
        {
            SetDirection(Direction.Up);

            if (!upWall)
            {
                shape.MoveTo(new Point(shape.CenterCoordinates.X, shape.CenterCoordinates.Y + countSteps));
                rightWall = false;
                lefttWall = false;
                downWall = false;
            }
        }

        public void GoDown(int countSteps)
        {
            SetDirection(Direction.Down);

            if (!downWall)
            {
                shape.MoveTo(new Point(shape.CenterCoordinates.X, shape.CenterCoordinates.Y - countSteps));
                rightWall = false;
                upWall = false;
                lefttWall = false;
            }
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
