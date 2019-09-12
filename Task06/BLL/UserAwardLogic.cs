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
            throw new NotImplementedException();
        }

        public IEnumerable<UserAward> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Award> GetAwardsByUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool UserRemoved(Guid userGuid)
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