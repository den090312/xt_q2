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
            EmptyStringCheck(name);
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

        public bool UserRemoved(Guid userGuid) => _userDao.UserRemoved(userGuid);

        public IEnumerable<User> GetAll()
        {
            var users = _userDao?.GetAll();
            NullCheck(users);

            return users;
        }

        public User GetUserByGuid(Guid userGuid)
        {
            var user = _userDao?.GetUserByGuid(userGuid);
            NullCheck(user);

            return user;
        }

        public void PrintInfo() => _userDao?.PrintInfo();

        private static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }

        private static void EmptyStringCheck(string inputString)
        {
            if (inputString == string.Empty)
            {
                throw new Exception($"{nameof(inputString)} is empty!");
            }
        }
    }
}