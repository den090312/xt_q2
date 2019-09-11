using InterfacesBLL;
using InterfacesDAL;
using Entities;
using System;

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

        public void AddUser(User user)
        {
            NullCheck(user);

            _userDao.AddUser(user);
        }

        public void RemoveUsers(string userName)
        {
            NullCheck(userName);

            _userDao.RemoveUsers(userName);
        }

        public void PrintUsers() => _userDao.PrintUsers();

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

        private static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }

        public string[] GetUserIdArray(string userName)
        {
            NullCheck(userName);

            return _userDao.GetUserIdArray(userName);
        }

        public string[] GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}