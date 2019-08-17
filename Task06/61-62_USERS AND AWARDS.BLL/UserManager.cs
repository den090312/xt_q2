using _61_62_USERS_AND_AWARDS.Common;
using _61_62_USERS_AND_AWARDS.Entities;
using _61_62_USERS_AND_AWARDS.Interfaces;
using System;

namespace _61_62_USERS_AND_AWARDS.BLL
{
    public class UserManager : IStorable, IUserable
    {
        private static IUserable Implementation { get; } = Dependencies.UserImplementation;

        private static IStorable StorageImplementation { get; } = Dependencies.StorageImplementation;

        public void CreateStorage() => StorageImplementation.CreateStorage();

        public void PrintStorageInfo() => StorageImplementation.PrintStorageInfo();

        public User CreateUser(string name, DateTime dateBirth)
        {
            NullCheck(name);
            CheckName(name);
            CheckDateOfBirth(dateBirth);

            return Implementation.CreateUser(name, dateBirth);
        }

        public void AddUser(User user)
        {
            NullCheck(user);
            Implementation.AddUser(user);
        }

        public void RemoveUser(string userName)
        {
            NullCheck(userName);
            Implementation.RemoveUser(userName);
        }

        public bool UserExists(string userName)
        {
            NullCheck(userName);

            return Implementation.UserExists(userName);
        }

        public void PrintUsers() => Implementation.PrintUsers();

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
