using _61_62_USERS_AND_AWARDS.Common;
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

        public void RemoveUser(string userName)
        {
            NullCheck(userName);

            var userID = userImplement.GetID(userName);
            NullCheck(userID);

            userImplement.RemoveUser(userName);

            if (userID != string.Empty)
            {
                awardImplement.RemoveUser(userID);
            }
        }

        public bool UserExists(string userName)
        {
            NullCheck(userName);

            return userImplement.UserExists(userName);
        }

        public void PrintUsers(List<KeyValuePair<string, string>> awardsList) => userImplement.PrintUsers(awardImplement.GetAwards());

        public void AddAwardToUser(string awardName, string userName)
        {
            NullCheck(awardName);
            NullCheck(userName);

            var awardID = awardImplement.GetID(awardName);
            NullCheck(awardID);

            if (awardID != string.Empty)
            {
                var userID = userImplement.GetID(userName);
                NullCheck(userID);

                if (userID != string.Empty && !RecordExists(awardID, userID))
                {
                    userImplement.AddAwardToUser(awardID, userID);
                    awardImplement.AddUserToAward(userID, awardID);
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
        public string GetID(string userName) => userImplement.GetID(userName);

        public bool RecordExists(string awardID, string userID)
        {
            NullCheck(awardID);
            NullCheck(userID);

            return userImplement.RecordExists(awardID, userID);
        }

        public void RemoveAward(string awardID)
        {
            NullCheck(awardID);
            userImplement.RemoveAward(awardID);
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
