namespace InterfacesBLL
{
    public interface IUserAwardLogic
    {
        void JoinAwardsToUsers(string awardName, string userName);

        void PrintUsersAwards();
    }
}