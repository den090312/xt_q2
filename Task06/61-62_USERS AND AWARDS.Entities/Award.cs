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
            AwardID = Guid.NewGuid().ToString();
            Title = title;
        }

        public static int GetFieldIndex(string fieldName)
        {
            switch (fieldName)
            {
                case "AwardID":
                    return 0;
                case "UserID":
                    return 1;
                case "Title":
                    return 2;
                default:
                    return -1;
            }
        }
    }
}
