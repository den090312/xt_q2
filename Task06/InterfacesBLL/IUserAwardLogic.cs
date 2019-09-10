namespace InterfacesBLL
{
    public interface IUserAwardLogic
    {
        void JoinAwardToUser(string awardID, string userID);

        void PrintUserAwards();

        bool RecordExists(string awardID, string userID);
    }
}