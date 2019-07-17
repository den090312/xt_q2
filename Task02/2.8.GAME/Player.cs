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

        private Dictionary<Player.Direction, bool> stops = GetStopsDictionary();

        private int health = 6;
        private Direction direction;

        public Dictionary<Direction, bool> Stops { get => stops; private set => stops = value; }

        public Player(Point userCenter)
        {
            shape = new Circle(userCenter, radius);
        }

        private static Dictionary<Player.Direction, bool> GetStopsDictionary()
        {
            var stops = new Dictionary<Player.Direction, bool>(4);

            stops.Add(Player.Direction.Up, true);
            stops.Add(Player.Direction.Down, true);
            stops.Add(Player.Direction.Left, true);
            stops.Add(Player.Direction.Right, true);

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
