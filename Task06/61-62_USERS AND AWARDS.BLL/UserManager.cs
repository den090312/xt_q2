using _61_62_USERS_AND_AWARDS.Entities;
using System;

namespace _61_62_USERS_AND_AWARDS.BLL
{
    public static class UserManager
    {
        public static readonly string separator;

        static UserManager()
        {
            separator = " | ";
        }

        public static void CreateUser(string name, DateTime birthDate) => StorageManager.AddUser(new User(name, birthDate));

    }
}
