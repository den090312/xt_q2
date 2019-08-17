using _61_62_USERS_AND_AWARDS.Entities;
using System;

namespace _61_62_USERS_AND_AWARDS.BLL
{
    public static class UserManager
    {
        public static void CreateUser(string name, DateTime birthDate)
        {
            StorageManager.NullCheck(name);

            CheckName(name);
            CheckDateOfBirth(birthDate);

            StorageManager.AddUser(new User(name, birthDate));
        }

        public static void DeleteUser(string name)
        {
            StorageManager.NullCheck(name);
            StorageManager.RemoveUser(name);
        }

        public static void PrintAllUsers() => StorageManager.PrintAllUsers();

        private static void CheckName(string name)
        {
            var userCharArray = name.ToCharArray();

            if (char.IsLower(userCharArray[0]))
            {
                throw new ArgumentException($"Filed '{name}' must begin from upper case!");
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
    }
}
