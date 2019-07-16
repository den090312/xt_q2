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

            if (IsOverlayed(player, apple))
            {
                player.GetHealth(1);
            }

            var bear = new Monster(new Circle(new Point(0, 4), 5), Monster.Type.Bear);

            player.GoRight(1);

            if (IsOverlayed(player, bear))
            {
                player.GetHealth(-3);
            }

            var tree = new Obstruction(new Circle(new Point(2, 4), 4), Obstruction.Type.Pit);

            player.GoUp(1);

            if (IsOverlayed(player, tree))
            {
                player.Stop();
            }
        }

        private static bool IsOverlayed(Subject sub1, Subject sub2) => sub1.Shape.CenterCoordinates == sub2.Shape.CenterCoordinates;

        private static bool TryToGo()
        {
            bool tryToGo = false;

            return tryToGo;
        }

        private static void GetEvent()
        {

        }
    }
}
