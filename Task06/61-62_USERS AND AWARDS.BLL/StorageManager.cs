using _61_62_USERS_AND_AWARDS.Interfaces;
using _61_62_USERS_AND_AWARDS.Entities;
using _61_62_USERS_AND_AWARDS.Common;
using System;


namespace _61_62_USERS_AND_AWARDS.BLL
{
    public static class StorageManager
    {
        public static IStorable StorageImplementation { get; }

        private static readonly string usersFilePath;

        private static readonly string awardsFilePath;

        static StorageManager()
        {
            StorageImplementation = Dependencies.StorageImplementation;
            usersFilePath = StorageImplementation.Users;
            awardsFilePath = StorageImplementation.Awards;
        }

        public static void CreateStorage() => StorageImplementation.CreateStorage();

        public static void PrintStoragePaths() => StorageImplementation.PrintStoragePaths();

        internal static void AddUser(User user)
        {
            NullCheck(user);

            StorageImplementation.AddUser(user);
        }

        public static void RemoveUser(string name)
        {
            NullCheck(name);

            StorageImplementation.RemoveElement(name, usersFilePath, "Users");
        }

        public static void PrintAllUsers() => StorageImplementation.PrintFileContent(usersFilePath);

        public static void AddAward(Award award)
        {
            NullCheck(award);

            StorageImplementation.AddAward(award);
        }

        public static void RemoveAward(string name)
        {
            NullCheck(name);

            StorageImplementation.RemoveElement(name, awardsFilePath, "Awards");
        }

        public static void PrintAllAwards() => StorageImplementation.PrintFileContent(awardsFilePath);

        public static void AddAwardToUser(string user, string award)
        {
            NullCheck(user);
            NullCheck(award);

            if (!UserExists(user, usersFilePath, "Users"))
            {
                throw new Exception($"{nameof(user)} not found!");
            }


            if (!AwardExists(award, awardsFilePath, "Awards"))
            {
                throw new Exception($"{nameof(award)} not found!");
            }

            StorageImplementation.AddAwardToUser(user, award);
        }

        public static bool UserExists(string elementName, string filePath, string fileName)
        {
            NullCheck(elementName);
            NullCheck(filePath);
            NullCheck(fileName);

            return StorageImplementation.ElementExists(elementName, filePath, fileName);
        }

        public static bool AwardExists(string elementName, string filePath, string fileName)
        {
            NullCheck(elementName);
            NullCheck(filePath);
            NullCheck(fileName);

            return StorageImplementation.ElementExists(elementName, filePath, fileName);
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
            Console.WriteLine();
            Console.WriteLine("\t8: exit");
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
