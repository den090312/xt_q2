using InterfacesBLL;
using InterfacesDAL;
using System;

namespace BLL
{
    public class UserAwardLogic : IUserAwardLogic
    {
        private readonly IUserAwardDao iUserAwardDao;

        public UserAwardLogic(IUserAwardDao iUserAwardDao)
        {
            this.iUserAwardDao = iUserAwardDao;
        }

        public void JoinAwardsToUsers(string[] awardIdArray, string[] userIdArray)
        {
            NullCheck(awardIdArray);
            NullCheck(userIdArray);

            iUserAwardDao.JoinAwardsToUsers(awardIdArray, userIdArray);
        }

        public void PrintUserAwards()
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