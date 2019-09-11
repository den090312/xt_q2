namespace InterfacesBLL
{
    public interface IUserAwardLogic
    {
        void JoinAwardToUser(string awardName, string userName);

        void PrintUsersAwards();
    }
}