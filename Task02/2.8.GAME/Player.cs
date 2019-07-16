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

        public void Move()
        {
            throw new NotImplementedException();
        }

        public void GoLeft(Player player, int countSteps)
        {
            player.shape.MoveTo(new Point(shape.CenterCoordinates.X - countSteps, shape.CenterCoordinates.Y));
        }

        public void GoRight(Player player, int countSteps)
        {
            player.shape.MoveTo(new Point(shape.CenterCoordinates.X + countSteps, shape.CenterCoordinates.Y));
        }

        public void GoUp(Player player, int countSteps)
        {
            player.shape.MoveTo(new Point(shape.CenterCoordinates.X, shape.CenterCoordinates.Y + countSteps));
        }

        public void GoDown(Player player, int countSteps)
        {
            player.shape.MoveTo(new Point(shape.CenterCoordinates.X, shape.CenterCoordinates.Y - countSteps));
        }
    }
}
