namespace Entities
{
    public class UserAward
    {
        public User UserRef { get; }

        public Award AwardRef { get; }

        public UserAward(User user, Award award)
        {
            UserRef = user;
            AwardRef = award;
        }
    }
}