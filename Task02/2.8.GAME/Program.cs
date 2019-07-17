using _2._1.ROUND;
using System;

namespace _2._8.GAME
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var player = new Player(new Point(0, 0));

            var apple = new Bonus(new Point(0, 0), Bonus.Type.Apple);
            player.GoRight(3);
            player = GetEvent(player, apple);

            var bear = new Monster(new Point(0, 0), Monster.Type.Bear);
            player.GoRight(1);
            player = GetEvent(player, bear);

            var tree = new Obstruction(new Point(0, 0), Obstruction.Type.Pit);
            player.GoUp(1);
            player = GetEvent(player, tree);
        }

        private static Player GetEvent(Player player, Bonus bonus)
        {
            if (IsOverlayed(player, bonus))
            {
                switch (bonus.type)
                {
                    case Bonus.Type.Apple:
                        player.GetHealth(3);
                        break;

                    case Bonus.Type.Limon:
                        player.GetHealth(2);
                        break;

                    case Bonus.Type.Cherry:
                        player.GetHealth(1);
                        break;
                }
            }

            return player;
        }

        private static Player GetEvent(Player player, Monster monster)
        {
            if (IsOverlayed(player, monster))
            {
                switch (monster.type)
                {
                    case Monster.Type.Bear:
                        player.GetHealth(-3);
                        break;

                    case Monster.Type.Wolf:
                        player.GetHealth(-2);
                        break;

                    case Monster.Type.Snake:
                        player.GetHealth(-1);
                        break;
                }
            }

            return player;
        }

        private static Player GetEvent(Player player, Obstruction obstruction)
        {
            if (IsOverlayed(player, obstruction))
            {
                player.Stop();
            }

            return player;
        }

        private static bool IsOverlayed(Subject sub1, Subject sub2) => sub1.shape.CenterCoordinates == sub2.shape.CenterCoordinates;
    }
}
