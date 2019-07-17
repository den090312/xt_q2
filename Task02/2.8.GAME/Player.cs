﻿using _2._1.ROUND;
using System;

namespace _2._8.GAME
{
    public class Player : Subject, IControllable
    {
        private int health = 6;

        public override Circle Shape { get; set; }

        public int Health { get => health; private set => health = value; }

        public Player(Circle playerShape)
        {
            Shape = playerShape;
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        public void GoLeft(int countSteps)
        {
            Shape.MoveTo(new Point(Shape.CenterCoordinates.X - countSteps, Shape.CenterCoordinates.Y));
        }

        public void GoRight(int countSteps)
        {
            Shape.MoveTo(new Point(Shape.CenterCoordinates.X + countSteps, Shape.CenterCoordinates.Y));
        }

        public void GoUp(int countSteps)
        {
            Shape.MoveTo(new Point(Shape.CenterCoordinates.X, Shape.CenterCoordinates.Y + countSteps));
        }

        public void GoDown(int countSteps)
        {
            Shape.MoveTo(new Point(Shape.CenterCoordinates.X, Shape.CenterCoordinates.Y - countSteps));
        }

        public void Stop()
        {

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
