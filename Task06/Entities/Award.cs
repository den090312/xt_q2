using System;

namespace Entities
{
    public class Award
    {
        public Guid AwardGuid { get; }

        public string Title { get; } = string.Empty;

        public Award(string title)
        {
            AwardGuid = Guid.NewGuid();
            Title = title;
        }
    }
}
