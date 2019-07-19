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

        public Obstruction(Field field, Point location, Type type)
        {
            FieldNullCheck(field);
            LocationNullCheck(location);
            LocationFieldCheck(field, location);

            Location = location;
            ObstructionType = type;
            field.AddSubject(this);
        }
    }
}

