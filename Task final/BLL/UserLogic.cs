using Entities;
using InterfacesBLL;
using InterfacesDAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserDao userDao;

        public UserLogic(IUserDao iUserDao)
        {
            NullCheck(iUserDao);

            userDao = iUserDao;
        }

        public bool Add(User user)
        {
            NullCheck(user);

            return userDao.Add(user);
        }

        public bool ChangeName(User user, string newName)
        {
            NullCheck(user);
            NullCheck(newName);
            EmptyStringCheck(newName);

            return userDao.UpdateName(user);
        }

        public IEnumerable<User> GetAll() => userDao.GetAll();

        public bool Remove(int UserId)
        {
            IdCheck(UserId);

            return userDao.Remove(UserId);
        }

        private void IdCheck(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException($"{nameof(id)} is incorrect!");
            }
        }

        private void EmptyStringCheck(string inputString)
        {
            if (inputString == string.Empty)
            {
                throw new ArgumentException($"{nameof(inputString)} is empty!");
            }
        }

        private void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
    }
}
