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
            _awardDao = awardDao;
        }

        public Award CreateAward(string title)
        {
            NullCheck(title);

            return new Award(title);
        }

        public void AddAward(Award award)
        {
            NullCheck(award);

            _awardDao.AddAward(award);
        }

        public void RemoveAwards(string title)
        {
            NullCheck(title);

            _awardDao.RemoveAwards(title);
        }

        public void PrintAwards() => _awardDao.PrintAwards();

        public string[] GetAwardIdArray(string awardName)
        {
            NullCheck(awardName);

            return _awardDao.GetAwardIdArray(awardName);
        }

        private static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }

        public string[] GetAllAwards()
        {
            throw new NotImplementedException();
        }
    }
}