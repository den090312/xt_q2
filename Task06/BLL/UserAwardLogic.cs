using InterfacesBLL;
using InterfacesDAL;
using System;

namespace BLL
{
    public class UserAwardLogic : IUserAwardLogic
    {
        private readonly IUserAwardDao _userAwardDao;
        private readonly IUserDao _userDao;
        private readonly IAwardDao _awardDao;

        public UserAwardLogic(IUserAwardDao userAwardDao, IUserDao userDao, IAwardDao awardDao)
        {
            NullCheck(userAwardDao);
            NullCheck(userDao);
            NullCheck(awardDao);

            _userAwardDao = userAwardDao;
            _userDao = userDao;
            _awardDao = awardDao;
        }

        public void JoinAwardToUser(string userName, string awardName)
        {
            NullCheck(userName);
            NullCheck(awardName);

            var userIdArray = _userDao.GetUserIdArray(userName);
            var awardIdArray = _awardDao.GetAwardIdArray(awardName);

            NullCheck(userIdArray);
            NullCheck(awardIdArray);

            _userAwardDao.JoinAwardsToUsers(userIdArray, awardIdArray);
        }

        public void PrintUsersAwards()
        {
            var userLines = _userDao.GetAllUsers();
            var awardLines = _awardDao.GetAllAwards();

            NullCheck(userLines);
            NullCheck(awardLines);

            _userAwardDao.PrintUsersAwards(userLines, awardLines);
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