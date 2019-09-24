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

        public Award Create(string title)
        {
            NullCheck(title);
            EmptyStringCheck(title);

            return new Award(title);
        }

        public bool Add(Award award)
        {
            NullCheck(award);

            return awardDao.Add(award);
        }

        public bool RemoveByGuid(Guid awardGuid) => awardDao.RemoveByGuid(awardGuid);

        public IEnumerable<Award> GetAll()
        {
            var awards = awardDao?.GetAll();
            NullCheck(awards);

            return awards;
        }

        public Award GetByGuid(Guid awardGuid)
        {
            var award = awardDao?.GetByGuid(awardGuid);
            NullCheck(award);

            return award;
        }

        public string GetInfo() => awardDao?.GetInfo();

        private static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
        private static void EmptyStringCheck(string inputString)
        {
            if (inputString == string.Empty)
            {
                throw new Exception($"{nameof(inputString)} is empty!");
            }
        }
    }
}