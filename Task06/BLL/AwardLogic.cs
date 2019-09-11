using InterfacesBLL;
using InterfacesDAL;
using Entities;
using System;

namespace BLL
{
    public class AwardLogic : IAwardLogic
    {
        private readonly IAwardDao iAwardDao;

        public AwardLogic(IAwardDao iAwardDao)
        {
            this.iAwardDao = iAwardDao;
        }

        public Award CreateAward(string title)
        {
            NullCheck(title);

            return new Award(title);
        }

        public void AddAward(Award award)
        {
            NullCheck(award);

            iAwardDao.AddAward(award);
        }

        public void RemoveAwards(string title)
        {
            NullCheck(title);

            iAwardDao.RemoveAwards(title);
        }
        public void PrintAwards() => iAwardDao.PrintAwards();

        private static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
    }
}