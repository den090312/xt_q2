﻿using InterfacesBLL;
using InterfacesDAL;
using Entities;
using System;

namespace BLL
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserDFO iUserDFO;

        public UserLogic(IUserDFO iUserDFO)
        {
            this.iUserDFO = iUserDFO;
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

            iUserDFO.AddUser(user);
        }

        public void PrintUsers() => iUserDFO.PrintUsers();

        public void RemoveUsers(string userName)
        {
            NullCheck(userName);

            throw new NotImplementedException();
        }

        public void EraseUser(string userID)
        {
            NullCheck(userID);

            iUserDFO.EraseUser(userID);
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

        private static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
    }
}
