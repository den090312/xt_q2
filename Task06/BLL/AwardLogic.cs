
using Entities;
using InterfacesBLL;
using System;

namespace BLL
{
    public class AwardLogic : IAwardLogic
    {
        public void AddAward(Award award)
        {
            NullCheck(award);

            throw new NotImplementedException();
        }

        public bool AwardsExists(string awardName)
        {
            NullCheck(awardName);

            throw new NotImplementedException();
        }

        public Award CreateAward(string title)
        {
            NullCheck(title);

            throw new NotImplementedException();
        }

        public void EraseAward(string awardID)
        {
            NullCheck(awardID);

            throw new NotImplementedException();
        }

        public string[] GetAwardIDArray(string awardName)
        {
            NullCheck(awardName);

            throw new NotImplementedException();
        }

        public void PrintAwards()
        {
            throw new NotImplementedException();
        }

        public void RemoveAwards(string awardName)
        {
            NullCheck(awardName);

            throw new NotImplementedException();
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