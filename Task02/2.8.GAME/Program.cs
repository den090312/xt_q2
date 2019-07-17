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
            field.ChangeSubjectLocation(Player.UseEvent(player, apple));
            WinCheck(player, field);

            var bear = new Monster(new Point(2, 0), Monster.Type.Bear);
            player.GoLeft(1);
            field.ChangeSubjectLocation(Player.UseEvent(player, bear));
            WinCheck(player, field);

            var stone = new Obstruction(new Point(2, 1), Obstruction.Type.Stone);
            player.GoUp(1);
            field.ChangeSubjectLocation(Player.UseEvent(player, stone));
            WinCheck(player, field);

            var cherry = new Bonus(new Point(0, 1), Bonus.Type.Cherry);
            player.GoDown(2);
            field.ChangeSubjectLocation(Player.UseEvent(player, cherry));
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
    }
}
