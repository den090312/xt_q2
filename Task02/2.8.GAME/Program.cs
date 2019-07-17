using _2._1.ROUND;

namespace _2._8.GAME
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var field = new Field(100, 100);

            var player = new Player(new Point(0, 0));

            var apple = new Bonus(new Point(0, 0), Bonus.Type.Apple);
            player.GoRight(3);
            player = UseEvent(player, apple);

            var bear = new Monster(new Point(3, 0), Monster.Type.Bear);
            player.GoRight(1);
            player = UseEvent(player, bear);

            var tree = new Obstruction(new Point(3, 1), Obstruction.Type.Pit);
            player.GoUp(1);
            player = UseEvent(player, tree);
        }

        private static Player UseEvent(Player player, Bonus bonus)
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
