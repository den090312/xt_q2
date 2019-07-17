using _2._1.ROUND;
using System;

namespace _2._8.GAME
{
    public class Player : Subject, IControllable
    {
        private int health = 6;
        private bool stop = false;
        private bool lefttWall = false;
        private bool rightWall = false;
        private bool upWall = false;
        private bool downWall = false;

        public int Health { get => health; private set => health = value; }
        public bool LefttWall { private get => lefttWall; set => lefttWall = value; }
        public bool RightWall { private get => rightWall; set => rightWall = value; }
        public bool UpWall { private get => upWall; set => upWall = value; }
        public bool DownWall { private get => downWall; set => downWall = value; }

        public Player(Point userCenter)
        {
            shape = new Circle(userCenter, radius);
        }

        public void Stop()
        {
            stop = true;
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        public void GoLeft(int countSteps)
        {
            if (!lefttWall)
            {
                shape.MoveTo(new Point(shape.CenterCoordinates.X - countSteps, shape.CenterCoordinates.Y));
                rightWall = false;
                upWall = false;
                downWall = false;
            }
        }

        public void GoRight(int countSteps)
        {
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
            if (!stop)
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
