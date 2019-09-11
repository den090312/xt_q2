namespace InterfacesDAL
{
    public interface IUserAwardDao
    {
        void JoinAwardsToUsers(string[] awardIdArray, string[] userIdArray);

        void PrintUserAwards();
    }
}