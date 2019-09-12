using System;

namespace Entities
{
    public class Award
    {
        public Guid AwardGuid { get; }

        public string Title { get; } = string.Empty;

        public Award(string title)
        {
            NullCheck(title);

            AwardGuid = Guid.NewGuid();
            Title = title;
        }

        private static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
    }
}
