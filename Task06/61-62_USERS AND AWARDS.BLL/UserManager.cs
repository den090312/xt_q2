using _61_62_USERS_AND_AWARDS.Entities;
using System;
using System.Collections.Generic;

namespace _61_62_USERS_AND_AWARDS.BLL
{
    public static class UserManager
    {
        public static List<User> Users { get; private set; }

        public static readonly string separator;

        static UserManager()
        {
            Users = new List<User>();
            separator = " | ";
        }

        public static void CreateUser(string name, DateTime birthDate) => StorageManager.AddUser(new User(name, birthDate));

        public static void PrintAllUsers()
        {
            foreach (var user in Users)
            {
                PrintSingleUser(user);
            }
        }

        public static void PrintSingleUser(User user)
        {
            Console.Write(user.Id + separator);
            Console.Write(user.Name + separator);
            Console.Write(user.DateOfBirth + separator);
            Console.Write(user.Age);
            Console.WriteLine();
        }
    }
}
