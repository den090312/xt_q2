namespace InterfacesBLL
{
    public interface IUserAwardLogic
    {
        void JoinAwardsToUsers(string[] awardIdArray, string[] userIdArray);

        void PrintUserAwards();
    }
}