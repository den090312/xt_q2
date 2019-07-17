using _2._1.ROUND;
using System;

namespace _2._8.GAME
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var field = new Field(100, 100);

            var player = new Player(new Point(0, 0));

            var apple = new Bonus(new Point(3, 0), Bonus.Type.Apple);
            player.GoRight(3);
            field.ChangeSubjectLocation(UseEvent(player, apple));
            WinCheck(player, field);

            var bear = new Monster(new Point(2, 0), Monster.Type.Bear);
            player.GoLeft(1);
            field.ChangeSubjectLocation(UseEvent(player, bear));
            WinCheck(player, field);

            var stone = new Obstruction(new Point(2, 1), Obstruction.Type.Stone);
            player.GoUp(1);
            field.ChangeSubjectLocation(UseEvent(player, stone));
            WinCheck(player, field);

            var cherry = new Bonus(new Point(0, 1), Bonus.Type.Cherry);
            player.GoDown(2);
            field.ChangeSubjectLocation(UseEvent(player, cherry));
            WinCheck(player, field);
        }

        private static void WinCheck(Player player, Field field)
        {
            if (player.Health > 0 & field.NoBonuses())
            {
                Console.WriteLine("WIN");
            }

            if (player.Health == 0)
            {
                Console.WriteLine("GAME OVER");
            }
        }

        private static Player UseEvent(Player player, Bonus bonus)
        {
            if (IsOverlayed(player, bonus))
            {
                switch (bonus.type)
                {
                    case Bonus.Type.Apple:
                        player.SetHealth(3);
                        break;

                    case Bonus.Type.Limon:
                        player.SetHealth(2);
                        break;

                    case Bonus.Type.Cherry:
                        player.SetHealth(1);
                        break;
                }
            }

            return player;
        }

        private static Player UseEvent(Player player, Obstruction obstruction)
        {
            if (IsOverlayed(player, obstruction))
            {
                player = SetStop(player);
            }

            return player;
        }

        private static Player UseEvent(Player player, Monster monster)
        {
            if (IsOverlayed(player, monster))
            {
                switch (monster.type)
                {
                    case Monster.Type.Bear:
                        player.SetHealth(-3);
                        break;

                    case Monster.Type.Wolf:
                        player.SetHealth(-2);
                        break;

                    case Monster.Type.Snake:
                        player.SetHealth(-1);
                        break;
                }
            }

            return player;
        }

        private static Player SetStop(Player player)
        {
            var direction = player.GetDirection();

            switch (direction)
            {
                case Player.Direction.Up:
                    player.Stops[Player.Direction.Up] = true;
                    break;

                case Player.Direction.Down:
                    player.Stops[Player.Direction.Down] = true;
                    break;

                case Player.Direction.Left:
                    player.Stops[Player.Direction.Left] = true;
                    break;

                case Player.Direction.Right:
                    player.Stops[Player.Direction.Right] = true;
                    break;
            }

            return player;
        }

        private static bool IsOverlayed(Subject sub1, Subject sub2) => sub1.Shape.CenterCoordinates == sub2.Shape.CenterCoordinates;
    }
}
