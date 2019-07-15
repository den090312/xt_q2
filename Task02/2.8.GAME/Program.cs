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
