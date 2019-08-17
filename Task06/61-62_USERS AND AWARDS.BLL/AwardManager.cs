using _61_62_USERS_AND_AWARDS.Common;
using _61_62_USERS_AND_AWARDS.Entities;
using _61_62_USERS_AND_AWARDS.Interfaces;
using System;

namespace _61_62_USERS_AND_AWARDS.BLL
{
    public class AwardManager : IStorable, IAwardable
    {
        private static readonly IAwardable implementation;

        private static readonly IStorable storageImplementation;

        public void CreateStorage() => storageImplementation.CreateStorage();

        public void PrintStorageInfo() => storageImplementation.PrintStorageInfo();

        static AwardManager()
        {
            implementation = Dependencies.AwardImplementation;
            storageImplementation = Dependencies.StorageImplementation;
        }

        public Award CreateAward(string title)
        {
            NullCheck(title);

            return new Award(title);
        }

        public void AddAward(Award award)
        {
            NullCheck(award);

            implementation.AddAward(award);
        }

        public void RemoveAward(string awardName)
        {
            NullCheck(awardName);
            implementation.RemoveAward(awardName);
        }

        public bool AwardExists(string awardName)
        {
            NullCheck(awardName);

            return implementation.AwardExists(awardName);
        }

        public void PrintAwards() => implementation.PrintAwards();

        public void AddUserToAward(string user)
        {
            NullCheck(user);
            implementation.AddUserToAward(user);
        }

        public static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
    }
}
