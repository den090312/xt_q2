using InterfacesBLL;
using InterfacesDAL;
using Entities;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class AwardLogic : IAwardLogic
    {
        private readonly IAwardDao awardDao;

        public AwardLogic(IAwardDao awardDao)
        {
            NullCheck(awardDao);

            this.awardDao = awardDao;
        }

        public Award CreateAward(string title)
        {
            NullCheck(title);

            return new Award(title);
        }

        public bool AwardAdded(Award award)
        {
            NullCheck(award);

            return awardDao.AwardAdded(award);
        }

        public bool AwardRemoved(Guid awardGuid) => awardDao.AwardRemoved(awardGuid);

        public IEnumerable<Award> GetAll() => awardDao.GetAll();

        public Award GetAwardByGuid(Guid awardGuid) => awardDao.GetAwardByGuid(awardGuid);

        public void PrintInfo() => awardDao.PrintInfo();

        private static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
    }
}