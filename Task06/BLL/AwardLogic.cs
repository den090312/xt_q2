using InterfacesBLL;
using InterfacesDAL;
using Entities;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class AwardLogic : IAwardLogic
    {
        private readonly IAwardDao _awardDao;

        public AwardLogic(IAwardDao awardDao)
        {
            NullCheck(awardDao);

            _awardDao = awardDao;
        }

        public Award CreateAward(string title)
        {
            NullCheck(title);

            return new Award(title);
        }

        public bool AwardAdded(Award award)
        {
            NullCheck(award);

            return _awardDao.AwardAdded(award);
        }

        public bool AwardRemoved(Guid awardGuid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Award> GetAll()
        {
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