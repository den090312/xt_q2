using _61_62_USERS_AND_AWARDS.Interfaces;
using System;
using System.IO;

namespace _61_62_USERS_AND_AWARDS.DAL
{
    public class FileStorage : IStorable
    {
        private static string Disk { get; }

        private static string Tom { get; }

        private static string Main { get; } 

        public static string Root { get; }

        private static readonly string messageNotFound;

        static FileStorage()
        {
            Disk = "D";
            Tom = $@"{Disk}:\";
            Main = $"{Tom}Task06";
            Root = $@"{Main}\Storage";
            messageNotFound = "not found";
        }

        public void CreateStorage()
        {
            if (!File.Exists(Main))
            {
                Directory.CreateDirectory(Main);
            }

            if (!File.Exists(Root))
            {
                Directory.CreateDirectory(Root);
            }
        }

        public void PrintStoragePaths()
        {
            Console.WriteLine("---------Task folders--------");

            bool isFolder = true;
            PrintSinglePath(Main, "Main", isFolder);
            PrintSinglePath(Root, "Root", isFolder);

            isFolder = false;
            PrintSinglePath(UserFileStorage.FilePath, "Users", isFolder);
            PrintSinglePath(AwardFileStrorage.FilePath, "Awards", isFolder);

            Console.WriteLine("-----------------------------");
        }

        private static void PrintSinglePath(string path, string name, bool isFolder)
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
