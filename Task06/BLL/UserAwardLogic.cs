using InterfacesBLL;
using InterfacesDAL;
using System;

namespace BLL
{
    public class UserAwardLogic : IUserAwardLogic
    {
        private readonly IUserAwardDao iUserAwardDao;
        private readonly IUserDao iUserDao;
        private readonly IAwardDao iAwardDao;

        public UserAwardLogic(IUserAwardDao iUserAwardDao, IUserDao iUserDao, IAwardDao iAwardDao)
        {
            this.iUserAwardDao = iUserAwardDao;
            this.iUserDao = iUserDao;
            this.iAwardDao = iAwardDao;
        }

        public void JoinAwardToUser(string userName, string awardName)
        {
            NullCheck(userName);
            NullCheck(awardName);

            var userIdArray = iUserDao.GetUserIdArray(userName);
            var awardIdArray = iAwardDao.GetAwardIdArray(awardName);

            NullCheck(userIdArray);
            NullCheck(awardIdArray);

            iUserAwardDao.JoinAwardsToUsers(userIdArray, awardIdArray);
        }

        public void PrintUsersAwards()
        {
            var userLines = iUserDao.GetAllUsers();
            var awardLines = iAwardDao.GetAllAwards();

            NullCheck(userLines);
            NullCheck(awardLines);

            iUserAwardDao.PrintUsersAwards(userLines, awardLines);
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