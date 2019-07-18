using _2._1.ROUND;
using System;

namespace _2._8.GAME
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var field = new Field(100, 100);
            var player = new Player(field, Point.Create(0, 0));

            var apple = new Bonus(field, Point.Create(3, 0), Bonus.Type.Apple);
            player.GoRight(3);
            player.TryGetBonus(field, apple);
            WinCheck(player, field);

            var bear = new Monster(field, Point.Create(2, 0), Monster.Type.Bear);
            player.GoLeft(1);
            player.TryMonsterEvent(bear);
            WinCheck(player, field);

            var stone = new Obstruction(field, Point.Create(2, 1), Obstruction.Type.Stone);
            player.GoUp(1);
            player.TryObstructionEvent(stone);
            WinCheck(player, field);

            var cherry = new Bonus(field, Point.Create(0, 1), Bonus.Type.Cherry);
            player.GoDown(2);
            player.TryGetBonus(field, cherry);
            WinCheck(player, field);
        }

        private static void WinCheck(Player player, Field field)
        {
            if (player.Health > 0 & field.NoBonuses())
            {
                Console.WriteLine("WIN");
            }

            if (player.Health > 0 & !field.NoBonuses())
            {
                Console.WriteLine("GAME COUNTINEWS");
            }

            if (player.Health == 0)
            {
                Console.WriteLine("GAME OVER");
            }
        }
    }
}