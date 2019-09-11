namespace InterfacesDAL
{
    public interface IUserAwardDao
    {
        void JoinAwardToUser(string awardID, string userID);

        void PrintUserAwards();
    }
}