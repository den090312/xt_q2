﻿using InterfacesBLL;
using InterfacesDAL;
using Entities;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserDao userDao;

        public UserLogic(IUserDao userDao)
        {
            NullCheck(userDao);

            this.userDao = userDao;
        }

        public User Create(string name, DateTime dateBirth)
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

        public bool Add(User user)
        {
            NullCheck(user);

            return userDao.Add(user);
        }

        public bool RemoveByGuid(Guid userGuid) => userDao.RemoveByGuid(userGuid);

        public IEnumerable<User> GetAll()
        {
            var users = userDao?.GetAll();
            NullCheck(users);

            return users;
        }

        public User GetByGuid(Guid userGuid)
        {
            var user = userDao?.GetByGuid(userGuid);
            NullCheck(user);

            return user;
        }

        public string GetInfo() => userDao?.GetInfo();

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