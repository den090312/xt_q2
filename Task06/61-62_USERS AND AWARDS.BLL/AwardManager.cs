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

            var awardArrayID = awardImplement.GetArrayID(awardName);
            NullCheck(awardArrayID);

            awardImplement.RemoveAward(awardName);

            foreach (var awardID in awardArrayID)
            {
                if (awardID != string.Empty)
                {
                    userImplement.EraseAward(awardID);
                }
            }
        }

        public bool AwardExists(string awardName)
        {
            NullCheck(awardName);

            return awardImplement.AwardExists(awardName);
        }

        public void PrintAwards() => awardImplement.PrintAwards();

        public void PinUserToAward(string userName, string awardName)
        {
            NullCheck(userName);
            NullCheck(awardName);

            var userArrayID = userImplement.GetArrayID(userName);
            NullCheck(userArrayID);

            foreach (var userID in userArrayID)
            {
                if (userID != string.Empty)
                {
                    var awardArrayID = awardImplement.GetArrayID(awardName);
                    NullCheck(awardArrayID);
                    PinProcessing(userID, awardArrayID);
                }
            }
        }

        private static void PinProcessing(string userID, string[] awardArrayID)
        {
            foreach (var arrayID in awardArrayID)
            {
                if (arrayID != string.Empty)
                {
                    awardImplement.PinUserToAward(userID, arrayID);
                }
            }
        }

        public string[] GetArrayID(string awardName) => awardImplement.GetArrayID(awardName);

        public List<KeyValuePair<string, string>> GetAwards() => awardImplement.GetAwards();

        public void EraseUser(string userID)
        {
            NullCheck(userID);
            awardImplement.EraseUser(userID);
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
