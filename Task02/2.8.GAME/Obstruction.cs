using _2._1.ROUND;

namespace _2._8.GAME
{
    public class Obstruction : Subject
    {
        public enum Type
        {
            Stone = 0,
            Tree = 1
        }

        public Type ObstructionType { get; private set; }

        public Obstruction(Field userField, Point location, Type userType)
        {
            Location = location;
            ObstructionType = userType;
            userField.AddSubject(this);
        }
    }
}

