using _2._1.ROUND;
using System;

namespace _2._8.GAME
{
    public class Player : Subject, IControllable
    {
        private int health = 6;
        private bool stop = false; 


        public int Health { get => health; private set => health = value; }

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
            if (!stop)
                shape.MoveTo(new Point(shape.CenterCoordinates.X - countSteps, shape.CenterCoordinates.Y));
        }

        public void GoRight(int countSteps)
        {
            if (!stop)
                shape.MoveTo(new Point(shape.CenterCoordinates.X + countSteps, shape.CenterCoordinates.Y));
        }

        public void GoUp(int countSteps)
        {
            if (!stop)
                shape.MoveTo(new Point(shape.CenterCoordinates.X, shape.CenterCoordinates.Y + countSteps));
        }

        public void GoDown(int countSteps)
        {
            if (!stop)
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
