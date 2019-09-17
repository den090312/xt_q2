using Entities;
using InterfacesBLL;
using InterfacesDAL;
using System;
using System.Collections.Generic;

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

        public bool JoinedAwardToUser(Guid userGuid, Guid awardGuid)
        {
            var user = _userDao?.GetUserByGuid(userGuid);
            NullCheck(user);

            var award = _awardDao?.GetAwardByGuid(awardGuid);
            NullCheck(award);

            return Joined(user, award);
        }

        private bool Joined(User user, Award award)
        {
            var singleAwardList = new List<Award>();
            singleAwardList.Add(award);

            var awardsByUser = _userAwardDao.GetAwardsByUser(user, singleAwardList);
            var counter = 0;

            foreach (var singleAward in awardsByUser)
            {
                counter++;
            }

            return counter > 0 ? false : _userAwardDao.JoinedAwardToUser(user, award);
        }

        public IEnumerable<Award> GetAwardsByUser(User user)
        {
            var awards = _awardDao?.GetAll();
            NullCheck(awards);

            return _userAwardDao?.GetAwardsByUser(user, awards);
        }

        public bool UserAwardsRemoved(Guid userGuid)
        {
            if (!_userDao.UserRemoved(userGuid))
            {
                return false;
            }

            var users = _userDao?.GetAll();
            NullCheck(users);

            var awards = _awardDao?.GetAll();
            NullCheck(awards);

            return _userAwardDao.UserAwardsRemoved(userGuid, users, awards);
        }

        public bool AwardUsersRemoved(Guid awardGuid)
        {
            if (!_awardDao.AwardRemoved(awardGuid))
            {
                return false;
            }

            var users = _userDao?.GetAll();
            NullCheck(users);

            var awards = _awardDao?.GetAll();
            NullCheck(awards);

            return _userAwardDao.AwardUsersRemoved(awardGuid, users, awards);
        }

        public void PrintInfo() => _userAwardDao?.PrintInfo();

        private static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
    }
}