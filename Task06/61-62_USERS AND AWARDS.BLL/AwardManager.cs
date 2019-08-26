using Task06.Common;
using Task06.Entities;
using Task06.Interfaces;
using System;
using System.Collections.Generic;

namespace Task06.BLL
{
    public class AwardManager : IAwardable
    {
        private static readonly IAwardable awardImplement;

        private static readonly IUserable userImplement;

        static AwardManager()
        {
            awardImplement = Dependencies.AwardImplement;
            userImplement = Dependencies.UserImplement;
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

        public void RemoveAwards(string awardName)
        {
            NullCheck(awardName);

            var awardArrayID = awardImplement.GetAwardIDArray(awardName);
            NullCheck(awardArrayID);

            awardImplement.RemoveAwards(awardName);

            foreach (var awardID in awardArrayID)
            {
                if (awardID != string.Empty)
                {
                    userImplement.EraseAward(awardID);
                }
            }
        }

        public bool AwardsExists(string awardName)
        {
            NullCheck(awardName);

            return awardImplement.AwardsExists(awardName);
        }

        public void PrintAwards() => awardImplement.PrintAwards();

        public void JoinUserToAward(string userName, string awardName)
        {
            NullCheck(userName);
            NullCheck(awardName);

            var userArrayID = userImplement.GetUserIDArray(userName);
            NullCheck(userArrayID);

            var awardArrayID = awardImplement.GetAwardIDArray(awardName);
            NullCheck(awardArrayID);

            foreach (var userID in userArrayID)
            {
                if (userID != string.Empty)
                {
                    Join(userID, awardArrayID);
                }
            }
        }

        private static void Join(string userID, string[] awardArrayID)
        {
            foreach (var arrayID in awardArrayID)
            {
                if (arrayID != string.Empty)
                {
                    awardImplement.JoinUserToAward(userID, arrayID);
                }
            }
        }

        public string[] GetAwardIDArray(string awardName)
        {
            NullCheck(awardName);
            return awardImplement.GetAwardIDArray(awardName);
        }

        public List<KeyValuePair<string, string>> GetAwardList() => awardImplement.GetAwardList();

        public void EraseUser(string userID)
        {
            NullCheck(userID);
            awardImplement.EraseUser(userID);
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
