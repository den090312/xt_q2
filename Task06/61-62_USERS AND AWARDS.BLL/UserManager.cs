using System;
using System.Collections.Generic;
using Task06.Common;
using Task06.Entities;
using Task06.Interfaces;

namespace Task06.BLL
{
    public class UserManager : IUserable
    {
        private static readonly IAwardable awardImplement;

        public static IUserable UserImplement { get; private set; }

        static UserManager()
        {
            UserImplement = Dependencies.UserImplement;
            awardImplement = Dependencies.AwardImplement;
        }

        private UserManager()
        {
        }

        public void CreateStorage() => UserImplement.CreateStorage();

        public void PrintStorageInfo() => UserImplement.PrintStorageInfo();

        public User CreateUser(string name, DateTime dateBirth)
        {
            NullCheck(name);
            CheckName(name);
            CheckDateOfBirth(dateBirth);

            return UserImplement.CreateUser(name, dateBirth);
        }

        public void AddUser(User user)
        {
            NullCheck(user);
            UserImplement.AddUser(user);
        }

        public void RemoveUsers(string userName)
        {
            NullCheck(userName);

            var userArrayID = UserImplement.GetUserIDArray(userName);
            NullCheck(userArrayID);

            UserImplement.RemoveUsers(userName);

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

            return UserImplement.UsersExists(userName);
        }

        public void PrintUsers(List<KeyValuePair<string, string>> awardsList)
        {
            NullCheck(awardsList);
            UserImplement.PrintUsers(awardsList);
        }

        public void Join(string awardName, string userName)
        {
            NullCheck(awardName);
            NullCheck(userName);

            var awardArrayID = awardImplement.GetAwardIDArray(awardName);
            NullCheck(awardArrayID);

            var userArrayID = UserImplement.GetUserIDArray(userName);
            NullCheck(userArrayID);

            foreach (var awardID in awardArrayID)
            {
                if (awardID != string.Empty)
                {
                    RunJoin(awardID, userArrayID);
                }
            }
        }

        private void RunJoin(string awardID, string[] userArrayID)
        {
            foreach (var userID in userArrayID)
            {
                if (userID != string.Empty && !RecordExists(awardID, userID))
                {
                    UserImplement.Join(awardID, userID);
                    awardImplement.Join(userID, awardID);
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

            return UserImplement.GetUserIDArray(userName);
        }

        public bool RecordExists(string awardID, string userID)
        {
            NullCheck(awardID);
            NullCheck(userID);

            return UserImplement.RecordExists(awardID, userID);
        }

        public void EraseAward(string awardID)
        {
            NullCheck(awardID);
            UserImplement.EraseAward(awardID);
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
