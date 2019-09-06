using Task06.Entities;
using Task06.Interfaces;
using System;
using System.Collections.Generic;
using Task06.DAL;

namespace Task06.BLL
{
    public class UserManager : IUserable
    {
        public static readonly IUserable userImplement;

        private static readonly IAwardable awardImplement;

        static UserManager()
        {
            userImplement = new UserFileStorage();
            awardImplement = new AwardFileStrorage();
        }

        public void CreateStorage() => userImplement.CreateStorage();

        public void PrintStorageInfo() => userImplement.PrintStorageInfo();

        public User CreateUser(string name, DateTime dateBirth)
        {
            NullCheck(name);
            CheckName(name);
            CheckDateOfBirth(dateBirth);

            return userImplement.CreateUser(name, dateBirth);
        }

        public void AddUser(User user)
        {
            NullCheck(user);
            userImplement.AddUser(user);
        }

        public void RemoveUsers(string userName)
        {
            NullCheck(userName);

            var userArrayID = userImplement.GetUserIDArray(userName);
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

        public void PrintUsers(List<KeyValuePair<string, string>> awardsList)
        {
            NullCheck(awardsList);
            userImplement.PrintUsers(awardsList);
        }

        public void JoinAwardToUser(string awardName, string userName)
        {
            NullCheck(awardName);
            NullCheck(userName);

            var awardArrayID = awardImplement.GetAwardIDArray(awardName);
            NullCheck(awardArrayID);

            var userArrayID = userImplement.GetUserIDArray(userName);
            NullCheck(userArrayID);

            foreach (var awardID in awardArrayID)
            {
                if (awardID != string.Empty)
                {
                    Join(awardID, userArrayID);
                }
            }
        }

        private void Join(string awardID, string[] userArrayID)
        {
            foreach (var userID in userArrayID)
            {
                if (userID != string.Empty && !RecordExists(awardID, userID))
                {
                    userImplement.JoinAwardToUser(awardID, userID);
                    awardImplement.JoinUserToAward(userID, awardID);
                }
            }
        }

        private static void CheckName(string name)
        {
            var userCharArray = name.ToCharArray();

            if (char.IsLower(userCharArray[0]))
            {
                throw new ArgumentException($"Field '{name}' must begin from upper case!");
            }
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

        public string[] GetUserIDArray(string userName)
        {
            NullCheck(userName);

            return userImplement.GetUserIDArray(userName);
        }

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

        private static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
    }
}
