namespace InterfacesBLL
{
    public interface IUserAwardLogic
    {
        void JoinRecords(string awardID, string userID);

        void PrintRecords();

        bool RecordExists(string awardID, string userID);
    }
}
