namespace InterfacesBLL
{
    public interface IUserAwardLogic
    {
        void JoinAwardToUser(string userName, string awardName);

        void PrintUsersAwards();
    }
}