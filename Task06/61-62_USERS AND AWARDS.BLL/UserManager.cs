﻿using _61_62_USERS_AND_AWARDS.Common;
using _61_62_USERS_AND_AWARDS.Entities;
using _61_62_USERS_AND_AWARDS.Interfaces;
using System;

namespace _61_62_USERS_AND_AWARDS.BLL
{
    public class UserManager : IUserable
    {
        private static readonly IUserable implementation;

        static UserManager() => implementation = Dependencies.UserImplementation;

        public void CreateStorage() => implementation.CreateStorage();

        public void PrintStorageInfo() => implementation.PrintStorageInfo();

        public User CreateUser(string name, DateTime dateBirth)
        {
            NullCheck(name);
            CheckName(name);
            CheckDateOfBirth(dateBirth);

            return new User(name, dateBirth);
        }

        public void AddUser(User user)
        {
            NullCheck(user);

            implementation.AddUser(user);
        }

        public void RemoveUser(string userName)
        {
            NullCheck(userName);
            implementation.RemoveUser(userName);
        }

        public bool UserExists(string userName)
        {
            NullCheck(userName);

            return implementation.UserExists(userName);
        }

        public void PrintUsers() => implementation.PrintUsers();

        public void AddAwardToUser(string awardName, string userName)
        {
            NullCheck(awardName);
            NullCheck(userName);
            implementation.AddAwardToUser(awardName, userName);
        }

        public static void CheckName(string name)
        {
            var userCharArray = name.ToCharArray();

            if (char.IsLower(userCharArray[0]))
            {
                throw new ArgumentException($"Filed '{name}' must begin from upper case!");
            }
        }

        public static void CheckDateOfBirth(DateTime birthDate)
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

        public static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
    }
}
