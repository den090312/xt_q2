
using _2._1.ROUND;
using System;

namespace _2._8.GAME
{
    public class Bonus : Subject
    {
        public Type BonusType { get; private set; }

        public enum Type
        {
            Apple = 0,
            Cherry = 1,
            Limon = 2
        }

        public Bonus() { }

        public Bonus(Field field, Point location, Type type)
        {
            FieldNullCheck(field);
            LocationNullCheck(location);
            LocationFieldCheck(field, location);

            Location = location;
            BonusType = type;
            field.AddSubject(this);
        }
    }
}
