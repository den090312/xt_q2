﻿using _61_62_USERS_AND_AWARDS.Common;
using _61_62_USERS_AND_AWARDS.Entities;
using _61_62_USERS_AND_AWARDS.Interfaces;
using System;
using System.Collections.Generic;

namespace _61_62_USERS_AND_AWARDS.BLL
{
    public class UserManager : IUserable
    {
        private static readonly IUserable userImplement;

        private static readonly IAwardable awardImplement;

        static UserManager()
        {
            userImplement = Dependencies.UserImplementation;
            awardImplement = Dependencies.AwardImplementation;
        }

        public void CreateStorage() => userImplement.CreateStorage();

        public void PrintStorageInfo() => userImplement.PrintStorageInfo();

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

            userImplement.AddUser(user);
        }

        public void RemoveUsers(string userName)
        {
            NullCheck(userName);

            var userArrayID = userImplement.GetArrayID(userName);
            NullCheck(userArrayID);

            userImplement.RemoveUsers(userName);

            foreach (var userID in userArrayID)
            {
                if (userID != string.Empty)
                {
                    awardImplement.EraseUser(userID);
                }
            }
        }

        public bool UsersExists(string userName)
        {
            NullCheck(userName);

            return userImplement.UsersExists(userName);
        }

        public void PrintUsers(List<KeyValuePair<string, string>> awardsList) => userImplement.PrintUsers(awardImplement.GetAwardList());

        public void PinAwardToUser(string awardName, string userName)
        {
            NullCheck(awardName);
            NullCheck(userName);

            var awardArrayID = awardImplement.GetArrayID(awardName);
            NullCheck(awardArrayID);

            foreach (var awardID in awardArrayID)
            {
                if (awardID != string.Empty)
                {
                    var userArrayID = userImplement.GetArrayID(userName);
                    NullCheck(userArrayID);
                    PinProcessing(awardID, userArrayID);
                }
            }
        }

        private void PinProcessing(string awardID, string[] userArrayID)
        {
            foreach (var userID in userArrayID)
            {
                if (userID != string.Empty && !RecordExists(awardID, userID))
                {
                    userImplement.PinAwardToUser(awardID, userID);
                    awardImplement.PinUserToAward(userID, awardID);
                }
            }
        }

        public static void CheckName(string name)
        {
            var userCharArray = name.ToCharArray();

            if (char.IsLower(userCharArray[0]))
            {
                throw new ArgumentException($"Field '{name}' must begin from upper case!");
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

        public string[] GetArrayID(string userName) => userImplement.GetArrayID(userName);

        public bool RecordExists(string awardID, string userID)
        {
            NullCheck(awardID);
            NullCheck(userID);

            return userImplement.RecordExists(awardID, userID);
        }

        public void EraseAward(string awardID)
        {
            NullCheck(awardID);
            userImplement.EraseAward(awardID);
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
