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

        public Player(Field userField, Point userCenter)
        {
            Shape = new Circle(userCenter, radius);
            userField.AddSubject(this);
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

        public Direction GetLocation() => direction;

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

        public void ChangeHealth(int amountHealth)
        {
            if (Health != 10)
            {
                Health += amountHealth;
            }
        }

        public void LaunchEvent(Bonus bonus)
        {
            if (IsOverlayed(this, bonus))
            {
                switch (bonus.BonusType)
                {
                    case Bonus.Type.Apple:
                        ChangeHealth(3);
                        break;

                    case Bonus.Type.Limon:
                        ChangeHealth(2);
                        break;

                    case Bonus.Type.Cherry:
                        ChangeHealth(1);
                        break;
                }
            }
        }

        public void LaunchEvent(Obstruction obstruction)
        {
            if (IsOverlayed(this, obstruction))
            {
                SetStop();
            }
        }

        public void LaunchEvent(Monster monster)
        {
            if (IsOverlayed(this, monster))
            {
                switch (monster.MonsterType)
                {
                    case Monster.Type.Bear:
                        ChangeHealth(-3);
                        break;

                    case Monster.Type.Wolf:
                        ChangeHealth(-2);
                        break;

                    case Monster.Type.Snake:
                        ChangeHealth(-1);
                        break;
                }
            }
        }

        public void SetStop()
        {
            var direction = GetLocation();

            switch (direction)
            {
                case Direction.Up:
                    Stops[Direction.Up] = true;
                    break;

                case Direction.Down:
                    Stops[Direction.Down] = true;
                    break;

                case Direction.Left:
                    Stops[Direction.Left] = true;
                    break;

                case Direction.Right:
                    Stops[Direction.Right] = true;
                    break;
            }

        }
    }
}
