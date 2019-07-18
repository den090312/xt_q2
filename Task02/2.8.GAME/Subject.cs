
using _2._1.ROUND;
using System;

namespace _2._8.GAME
{
    public abstract class Subject
    {
        public Point Location { get; protected set; } = new Point(0, 0);

        protected static bool IsOverlayed(Subject sub1, Subject sub2) => sub1.Location == sub2.Location;

        protected void FieldNullCheck(Field field)
        {
            if (field is null)
            {
                throw new ArgumentNullException($"{nameof(field)} is null!");
            }
        }

        protected void MonsterNullCheck(Monster monster)
        {
            if (monster is null)
            {
                throw new ArgumentNullException($"{nameof(monster)} is null!");
            }
        }

        protected void BonusNullCheck(Bonus bonus)
        {
            if (bonus is null)
            {
                throw new ArgumentNullException($"{nameof(bonus)} is null!");
            }
        }

        protected void ObstructionNullCheck(Obstruction obstruction)
        {
            if (obstruction is null)
            {
                throw new ArgumentNullException($"{nameof(obstruction)} is null!");
            }
        }
    }
}
