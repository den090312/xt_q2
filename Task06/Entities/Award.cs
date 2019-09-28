using System;

namespace Entities
{
    public class Award
    {
        public Guid Guid { get; }

        public string Title { get; }

        public Award(string title)
        {
            Guid = Guid.NewGuid();
            Title = title;
        }

        public Award(Guid guid, string title)
        {
            Guid = guid;
            Title = title;
        }
    }
}