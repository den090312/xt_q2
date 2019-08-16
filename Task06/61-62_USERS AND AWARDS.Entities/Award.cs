using System;

namespace _61_62_USERS_AND_AWARDS.Entities
{
    public class Award
    {
        public string AwardID { get; } = string.Empty;

        public string UserID { get; } = string.Empty;

        public string Title { get; } = string.Empty;

        public Award(string title)
        {
            User.NullCheck(title);

            AwardID = Guid.NewGuid().ToString();
            Title = title;
        }
    }
}
