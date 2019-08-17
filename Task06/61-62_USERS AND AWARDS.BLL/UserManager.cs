using _61_62_USERS_AND_AWARDS.Common;
using _61_62_USERS_AND_AWARDS.Entities;
using _61_62_USERS_AND_AWARDS.Interfaces;
using System;

namespace _61_62_USERS_AND_AWARDS.BLL
{
    public class UserManager : IStorable, IUserable
    {
        public static IUserable UserImplementation { get; } = Dependencies.UserImplementation;

        public static IStorable StorageImplementation { get; } = Dependencies.StorageImplementation;

        public void CreateStorage() => StorageImplementation.CreateStorage();

        public void AddUser(User user)
        {
            NullCheck(user);
            UserImplementation.AddUser(user);
        }

        public void RemoveUser(string userName)
        {
            NullCheck(userName);
            UserImplementation.RemoveUser(userName);
        }

        public bool UserExists(string userName)
        {
            NullCheck(userName);

            return UserImplementation.UserExists(userName);
        }

        public void PrintUsers() => UserImplementation.PrintUsers();

        public static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
    }
}
