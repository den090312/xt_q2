using System;

namespace Entities
{
    public class Award
    {
        public string AwardId { get; } = string.Empty;

        public string Title { get; } = string.Empty;

        public Award(string title)
        {
            AwardId = Guid.NewGuid().ToString();
            Title = title;
        }

        public static int GetFieldIndex(string fieldName)
        {
            switch (fieldName)
            {
                case "AwardID":
                    return 0;
                case "Title":
                    return 1;
                default:
                    return -1;
            }
        }
    }
}
