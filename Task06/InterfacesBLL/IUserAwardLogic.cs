namespace InterfacesBLL
{
    public interface IUserAwardLogic
    {
        void JoinAwardToUser(string awardID, string userID);

        void PrintUserAwards();
    }
}