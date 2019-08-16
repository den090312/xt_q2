using _61_62_USERS_AND_AWARDS.Entities;
using System;

namespace _61_62_USERS_AND_AWARDS.BLL
{
    public static class UserManager
    {
        public static void CreateUser(string name, DateTime birthDate)
        {
            StorageManager.NullCheck(name);
            StorageManager.AddUser(new User(name, birthDate));
        }

        public static void DeleteUser(string name)
        {
            StorageManager.NullCheck(name);
            StorageManager.RemoveUser(name);
        }

        public static void PrintAllUsers() => StorageManager.PrintAllUsers();
    }
}
