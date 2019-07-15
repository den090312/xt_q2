using System;

namespace _2._8.GAME
{
    public class Program
    {
        private static void Main(string[] args)
        {

        }

        public class Field
        {
            private int width;
            private int height;

            private Field(int fieldWidth, int fieldHeight)
            {
                if (fieldWidth < 0)
                {
                    throw new Exception("Ширина поля не может быть меньше нуля!");
                }

                if (fieldHeight < 0)
                {
                    throw new Exception("Высота поля не может быть меньше нуля!");
                }

                width = fieldWidth;
                height = fieldHeight;
            }
        }

        public abstract class Subject
        {
            private readonly bool movable;
        }

        public class Player : Subject
        {
            private readonly bool movable = true;
            private int health;
            private int speed;
        }

        public class Bonus : Subject
        {
            private readonly bool movable = false;
        }

        public class Monster : Subject
        {
            private readonly bool movable = true;
        }

        public class Obstruction : Subject
        {
            private readonly bool movable = false;
        }
    }
}
