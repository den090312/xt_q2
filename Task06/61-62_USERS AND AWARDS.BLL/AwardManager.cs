using System;
using System.Collections.Generic;
using Task06.Common;
using Task06.Entities;
using Task06.Interfaces;

namespace Task06.BLL
{
    public class AwardManager : IAwardable
    {
        private static readonly IUserable userImplement;

        public static IAwardable AwardImplement { get; private set; }

        static AwardManager()
        {
            AwardImplement = Dependencies.AwardImplement;
            userImplement = Dependencies.UserImplement;
        }

        private AwardManager()
        {
        }

        public void CreateStorage() => AwardImplement.CreateStorage();

        public void PrintStorageInfo() => AwardImplement.PrintStorageInfo();

        public Award CreateAward(string title)
        {
            NullCheck(title);

            return AwardImplement.CreateAward(title);
        }

        public void AddAward(Award award)
        {
            NullCheck(award);
            AwardImplement.AddAward(award);
        }

        public void RemoveAwards(string awardName)
        {
            NullCheck(awardName);

            var awardArrayID = AwardImplement.GetAwardIDArray(awardName);
            NullCheck(awardArrayID);

            AwardImplement.RemoveAwards(awardName);

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

            return AwardImplement.AwardsExists(awardName);
        }

        public void PrintAwards() => AwardImplement.PrintAwards();

        public void Join(string userName, string awardName)
        {
            NullCheck(userName);
            NullCheck(awardName);

            var userArrayID = userImplement.GetUserIDArray(userName);
            NullCheck(userArrayID);

            var awardArrayID = AwardImplement.GetAwardIDArray(awardName);
            NullCheck(awardArrayID);

            foreach (var userID in userArrayID)
            {
                if (userID != string.Empty)
                {
                    RunJoin(userID, awardArrayID);
                }
            }
        }

        private static void RunJoin(string userID, string[] awardArrayID)
        {
            foreach (var arrayID in awardArrayID)
            {
                if (arrayID != string.Empty)
                {
                    AwardImplement.Join(userID, arrayID);
                }
            }
        }

        public string[] GetAwardIDArray(string awardName)
        {
            NullCheck(awardName);

            return AwardImplement.GetAwardIDArray(awardName);
        }

        public List<KeyValuePair<string, string>> GetAwardList() => AwardImplement.GetAwardList();

        public void EraseUser(string userID)
        {
            NullCheck(userID);
            AwardImplement.EraseUser(userID);
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
