namespace Entities
{
    public class UserAward
    {
        public string UserID { get; } = string.Empty;

        public string AwardID { get; } = string.Empty;

        public UserAward(string userID, string awardID)
        {
            UserID = userID;
            AwardID = awardID;
        }
    }
}
