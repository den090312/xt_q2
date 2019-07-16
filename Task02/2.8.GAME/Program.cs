using _2._1.ROUND;
using System;

namespace _2._8.GAME
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var player = new Player(new Circle(new Point(0, 0), 3));

            var apple = new Bonus(new Circle(new Point(0, 3), 3), Bonus.Type.Apple);

            player.GoRight(3);

            if (player.Shape.CenterCoordinates == apple.Shape.CenterCoordinates)
            {
                player.GetHealth(1);
            }

            var bear = new Monster(new Circle(new Point(0, 4), 5), Monster.Type.Bear);

            player.GoRight(1);

            if (player.Shape.CenterCoordinates == bear.Shape.CenterCoordinates)
            {
                player.GetHealth(-3);
            }

            var tree = new Obstruction(new Circle(new Point(2, 4), 4), Obstruction.Type.Pit);

            player.GoUp(1);

            if (player.Shape.CenterCoordinates != tree.Shape.CenterCoordinates)
            {
                player.GoUp(1);
            }
        }
    }
}
