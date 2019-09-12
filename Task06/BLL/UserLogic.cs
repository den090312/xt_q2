using InterfacesBLL;
using InterfacesDAL;
using Entities;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserDao _userDao;

        public UserLogic(IUserDao userDao)
        {
            NullCheck(userDao);

            _userDao = userDao;
        }

        public User CreateUser(string name, DateTime dateBirth)
        {
            NullCheck(name);
            CheckDateOfBirth(dateBirth);

            return new User(name, dateBirth);
        }

        private static void CheckDateOfBirth(DateTime birthDate)
        {
            DateTime currentDateTime = DateTime.Now.Date;

            if (birthDate > currentDateTime)
            {
                throw new ArgumentException("Date of birth can't be more than current date!");
            }

            if (birthDate == currentDateTime)
            {
                throw new ArgumentException("Welcome to our world!");
            }
        }

        public bool UserAdded(User user)
        {
            NullCheck(user);

            return _userDao.UserAdded(user);
        }

        public bool UserRemoved(Guid userGuid)
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

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}