using _61_62_USERS_AND_AWARDS.Interfaces;
using _61_62_USERS_AND_AWARDS.Entities;
using _61_62_USERS_AND_AWARDS.Common;
using System;


namespace _61_62_USERS_AND_AWARDS.BLL
{
    public static class StorageManager
    {
        public static IStorable StorageImplementation { get; }

        private static readonly string usersFile;

        private static readonly string awardsFile;

        static StorageManager()
        {
            StorageImplementation = Dependencies.StorageImplementation;
            usersFile = StorageImplementation.Users;
            awardsFile = StorageImplementation.Awards;
        }

        public static void CreateStorage() => StorageImplementation.CreateStorage();

        public static void PrintStoragePaths() => StorageImplementation.PrintStoragePaths();

        public static void AddUser(User user) => StorageImplementation.AddUser(user);

        public static void RemoveUser(string name) => StorageImplementation.RemoveElement(name, usersFile, "Users");

        public static void PrintAllUsers() => StorageImplementation.PrintFileContent(usersFile);

        public static void AddAward(Award award) => StorageImplementation.AddAward(award);

        public static void RemoveAward(string name) => StorageImplementation.RemoveElement(name, awardsFile, "Awards");

        public static void PrintAllAwards() => StorageImplementation.PrintFileContent(awardsFile);

        public static void AddAwardToUser(string user, string award)
        {
            if (UserManager.UserExists(user) & AwardManager.AwardExists(award))
            {
                StorageImplementation.AddAwardToUser(user, award);
            }
        }

        public static void WriteMenu()
        {
            Console.WriteLine("Users operations:");
            Console.WriteLine("\t1: create");
            Console.WriteLine("\t2: delete");
            Console.WriteLine("\t3: print");
            Console.WriteLine();
            Console.WriteLine("Awards operations:");
            Console.WriteLine("\t4: create");
            Console.WriteLine("\t5: delete");
            Console.WriteLine("\t6: print");
            Console.WriteLine();
            Console.WriteLine("Connect operations:");
            Console.WriteLine("\t7: add award to user");
            Console.WriteLine("\t8: add user to award");
            Console.WriteLine();
            Console.WriteLine("\t9: exit");
        }
    }
}
