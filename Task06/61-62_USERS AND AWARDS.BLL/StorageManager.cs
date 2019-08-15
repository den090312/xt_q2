using _61_62_USERS_AND_AWARDS.Interfaces;
using _61_62_USERS_AND_AWARDS.Entities;
using _61_62_USERS_AND_AWARDS.Common;
using _61_62_USERS_AND_AWARDS.DAL;
using System;
using System.IO;
using System.Threading;

namespace _61_62_USERS_AND_AWARDS.BLL
{
    public static class StorageManager
    {
        private static readonly string usersFile = Storage.Users;

        private static readonly string separator = UserManager.separator;

        public static IStorable CurrentStorage { get; } = Dependencies.CurrentStorage;

        public static void Create() => CurrentStorage.Create();

        public static void PrintObjects() => CurrentStorage.PrintAllPaths();

        public static void AddUser(User user)
        {
            CreateUsersFile();

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(usersFile, true);

            streamWriter.Write(user.Id + separator);
            streamWriter.Write(user.Name + separator);
            streamWriter.Write(user.DateOfBirth + separator);
            streamWriter.Write(user.Age);
            streamWriter.WriteLine();

            streamWriter.Close();
        }

        private static void CreateUsersFile()
        {
            if (!File.Exists(usersFile))
            {
                Thread.Sleep(10);
                var streamWriter = new StreamWriter(usersFile, true);

                streamWriter.Write("");
                streamWriter.Close();
            }
            else
            {
                Thread.Sleep(10);
                var attributes = File.GetAttributes(usersFile);

                if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    Thread.Sleep(10);
                    File.SetAttributes(usersFile, FileAttributes.Normal);
                }
            }
        }

        public static void WriteMenu()
        {
            Console.WriteLine("User operations:");
            Console.WriteLine("\t1: create");
            Console.WriteLine("\t2: delete");
            Console.WriteLine("\t3: exit");
        }
    }
}
