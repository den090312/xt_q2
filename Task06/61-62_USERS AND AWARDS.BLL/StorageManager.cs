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

        public static void PrintAllAwards() => StorageImplementation.PrintUsers(awardsFilePath);

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

            return StorageImplementation.UserExists(elementName, filePath, fileName);
        }

        public static bool AwardExists(string elementName, string filePath, string fileName)
        {
            NullCheck(elementName);
            NullCheck(filePath);
            NullCheck(fileName);

            return StorageImplementation.UserExists(elementName, filePath, fileName);
        }
    }
}
