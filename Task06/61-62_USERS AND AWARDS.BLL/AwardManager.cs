using _61_62_USERS_AND_AWARDS.Common;
using _61_62_USERS_AND_AWARDS.Entities;
using _61_62_USERS_AND_AWARDS.Interfaces;
using System;

namespace _61_62_USERS_AND_AWARDS.BLL
{
    public class AwardManager : IStorable, IAwardable
    {
        private static IAwardable Implementation { get; } = Dependencies.AwardImplementation;

        private static IStorable StorageImplementation { get; } = Dependencies.StorageImplementation;

        public void CreateStorage() => StorageImplementation.CreateStorage();

        public void PrintStorageInfo() => StorageImplementation.PrintStorageInfo();

        public Award CreateAward(string title)
        {
            NullCheck(title);

            return new Award(title);
        }

        public void AddAward(Award award)
        {
            NullCheck(award);

            Implementation.AddAward(award);
        }

        public void RemoveAward(string awardName)
        {
            NullCheck(awardName);
            Implementation.RemoveAward(awardName);
        }

        public bool AwardExists(string awardName)
        {
            NullCheck(awardName);

            return Implementation.AwardExists(awardName);
        }

        public void PrintAwards() => Implementation.PrintAwards();

        public static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
    }
}
