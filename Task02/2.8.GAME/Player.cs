using _2._1.ROUND;
using System;
using System.Collections.Generic;

namespace _2._8.GAME
{
    public class Player : Subject, IControllable
    {
        public enum Direction
        {
            Up = 0,
            Down = 1,
            Left = 2,
            Right = 3
        }

        private Direction direction;

        public int Health { get; private set; } = 6;

        public Dictionary<Direction, bool> Stops { get; private set; } = GetStopsDictionary();

        public Player(Point userCenter)
        {
            Shape = new Circle(userCenter, radius);
        }

        private static Dictionary<Direction, bool> GetStopsDictionary()
        {
            var stops = new Dictionary<Direction, bool>(4)
            {
                { Player.Direction.Up, false },
                { Player.Direction.Down, false },
                { Player.Direction.Left, false },
                { Player.Direction.Right, false }
            };

            return stops;
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        public Direction GetDirection() => direction;

        public void GoLeft(int countSteps)
        {
            direction = Direction.Left;

            if (Stops[direction] == false)
            {
                Shape.MoveTo(new Point(Shape.CenterCoordinates.X - countSteps, Shape.CenterCoordinates.Y));
            }
        }

        public void GoRight(int countSteps)
        {
            direction = Direction.Right;

            if (Stops[direction] == false)
            {
                Shape.MoveTo(new Point(Shape.CenterCoordinates.X + countSteps, Shape.CenterCoordinates.Y));
            }
        }

        public void GoUp(int countSteps)
        {
            direction = Direction.Up;

            if (Stops[direction] == false)
            {
                Shape.MoveTo(new Point(Shape.CenterCoordinates.X, Shape.CenterCoordinates.Y + countSteps));
            }
        }

        public void GoDown(int countSteps)
        {
            direction = Direction.Down;

            if (Stops[direction] == false)
            {
                Shape.MoveTo(new Point(Shape.CenterCoordinates.X, Shape.CenterCoordinates.Y - countSteps));
            }
        }

        public void SetHealth(int amountHealth)
        {
            if (Health != 10)
            {
                Health += amountHealth;
            }
        }
    }
}
