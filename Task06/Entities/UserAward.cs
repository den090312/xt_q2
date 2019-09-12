using System;

namespace Entities
{
    public class UserAward
    {
        public User UserRef { get; }

        public Award AwardRef { get; }

        public UserAward(User user, Award award)
        {
            NullCheck(user);
            NullCheck(award);

            UserRef = user;
            AwardRef = award;
        }

        private static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
    }
}