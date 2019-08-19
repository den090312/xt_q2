using _61_62_USERS_AND_AWARDS.Common;
using _61_62_USERS_AND_AWARDS.Entities;
using _61_62_USERS_AND_AWARDS.Interfaces;
using System;
using System.Collections.Generic;

namespace _61_62_USERS_AND_AWARDS.BLL
{
    public class AwardManager : IAwardable
    {
        private static readonly IAwardable awardImplement;

        private static readonly IUserable userImplement;

        static AwardManager()
        {
            awardImplement = Dependencies.AwardImplementation;
            userImplement = Dependencies.UserImplementation;
        }

        public void CreateStorage() => awardImplement.CreateStorage();

        public void PrintStorageInfo() => awardImplement.PrintStorageInfo();

        public Award CreateAward(string title)
        {
            NullCheck(title);

            return new Award(title);
        }

        public void AddAward(Award award)
        {
            NullCheck(award);

            awardImplement.AddAward(award);
        }

        public void RemoveAward(string awardName)
        {
            NullCheck(awardName);
            awardImplement.RemoveAward(awardName);
        }

        public bool AwardExists(string awardName)
        {
            NullCheck(awardName);

            return awardImplement.AwardExists(awardName);
        }

        public void PrintAwards() => awardImplement.PrintAwards();

        public void AddUserToAward(string userName, string awardName)
        {
            NullCheck(userName);
            NullCheck(awardName);

            var userID = userImplement.GetID(userName);
            NullCheck(userID);

            if (userID != string.Empty)
            {
                var awardID = awardImplement.GetID(awardName);
                NullCheck(awardID);

                if (awardID != string.Empty)
                {
                    awardImplement.AddUserToAward(userID, awardID);
                }
            }
        }

        public string GetID(string awardName) => awardImplement.GetID(awardName);

        public List<KeyValuePair<string, string>> GetAwards() => awardImplement.GetAwards();

        public static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
    }
}
