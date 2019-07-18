using _2._1.ROUND;
using System;

namespace _2._8.GAME
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var gameField = new Field(100, 100);
            var player = new Player(gameField, Point.Create(0, 0));

            var apple = new Bonus(gameField, Point.Create(3, 0), Bonus.Type.Apple);
            player.GoRight(3);
            gameField.ChangeSubjectLocation(Player.UseEvent(player, apple));
            WinCheck(player, gameField);

            var bear = new Monster(gameField, Point.Create(2, 0), Monster.Type.Bear);
            player.GoLeft(1);
            gameField.ChangeSubjectLocation(Player.UseEvent(player, bear));
            WinCheck(player, gameField);

            var stone = new Obstruction(Point.Create(2, 1), Obstruction.Type.Stone);
            player.GoUp(1);
            gameField.ChangeSubjectLocation(Player.UseEvent(player, stone));
            WinCheck(player, gameField);

            var cherry = new Bonus(gameField, Point.Create(0, 1), Bonus.Type.Cherry);
            player.GoDown(2);
            gameField.ChangeSubjectLocation(Player.UseEvent(player, cherry));
            WinCheck(player, gameField);
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