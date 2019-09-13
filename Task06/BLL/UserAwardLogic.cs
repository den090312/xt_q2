using Entities;
using InterfacesBLL;
using InterfacesDAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class UserAwardLogic : IUserAwardLogic
    {
        private readonly IUserAwardDao userAwardDao;
        private readonly IUserDao userDao;
        private readonly IAwardDao awardDao;

        public UserAwardLogic(IUserAwardDao userAwardDao, IUserDao userDao, IAwardDao awardDao)
        {
            NullCheck(userAwardDao);
            NullCheck(userDao);
            NullCheck(awardDao);

            this.userAwardDao = userAwardDao;
            this.userDao = userDao;
            this.awardDao = awardDao;
        }

        public bool JoinedAwardToUser(Guid userGuid, Guid awardGuid)
        {
            var user = userDao.GetUserByGuid(userGuid);
            NullCheck(user);

            var award = awardDao.GetAwardByGuid(awardGuid);
            NullCheck(award);

            return userAwardDao.JoinedAwardToUser(user, award);
        }

        public IEnumerable<UserAward> GetAll()
        {
            var users = userDao.GetAll();
            NullCheck(users);

            var awards = awardDao.GetAll();
            NullCheck(awards);

            return userAwardDao.GetAll(users, awards);
        }

        public IEnumerable<Award> GetAwardsByUser(User user)
        {
            var awards = awardDao.GetAll();
            NullCheck(awards);

            return userAwardDao.GetAwardsByUser(user, awards);
        }

        public bool UserRemoved(Guid userGuid)
        {
            var users = userDao.GetAll();
            NullCheck(users);

            var awards = awardDao.GetAll();
            NullCheck(awards);

            return userAwardDao.UserRemoved(userGuid, users, awards);
        }

        public bool AwardRemoved(Guid awardGuid)
        {
            throw new NotImplementedException();
        }

        public void PrintInfo() => userAwardDao.PrintInfo();

        private static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
    }
}