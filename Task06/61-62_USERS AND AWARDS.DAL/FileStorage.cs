using _61_62_USERS_AND_AWARDS.Interfaces;
using System;
using System.IO;

namespace _61_62_USERS_AND_AWARDS.DAL
{
    public class FileStorage : IStorable
    {
        private readonly string messageNotFound = "not found!";

        private static string Disk { get; }

        public static string TaskPath { get; }

        static FileStorage()
        {
            Disk = "D";
            TaskPath = $@"{Disk}:\Task06";
        }

        public void CreateStorage()
        {
            if (!File.Exists(TaskPath))
            {
                Directory.CreateDirectory(TaskPath);
            }
        }

        public void PrintStorageInfo()
        {
            Console.WriteLine("---------Task folders--------");

            bool isFolder = true;
            PrintSinglePath(TaskPath, "Main", isFolder);

            isFolder = false;
            PrintSinglePath(UserFileStorage.FilePath, "Users", isFolder);
            PrintSinglePath(AwardFileStrorage.FilePath, "Awards", isFolder);

            Console.WriteLine("-----------------------------");
        }

        private void PrintSinglePath(string path, string name, bool isFolder)
        {
            if (isFolder & Directory.Exists(path))
            {
                Console.WriteLine($"{name} - {path}");
            }
            else if (!isFolder & File.Exists(path))
            {
                Console.WriteLine($"{name} - {path}");
            }
            else
            {
                Console.WriteLine($"{name} - {messageNotFound}");
            }
        }
    }
}
