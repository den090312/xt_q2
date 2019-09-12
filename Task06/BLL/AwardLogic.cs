using InterfacesBLL;
using InterfacesDAL;
using Entities;
using System;

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
            throw new NotImplementedException();
        }

        public bool AwardRemoved(Guid awardGuid)
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