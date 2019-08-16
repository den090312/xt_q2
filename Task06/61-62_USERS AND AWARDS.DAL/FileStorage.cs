using _61_62_USERS_AND_AWARDS.Entities;
using _61_62_USERS_AND_AWARDS.Interfaces;
using System;
using System.IO;
using System.Threading;

namespace _61_62_USERS_AND_AWARDS.DAL
{
    public class FileStorage : IStorable
    {
        private static string Disk { get; }

        private static string Tom { get; }

        private static string Main { get; } 

        private static string Root { get; }

        public string Users { get; } = $@"{Main}\Users.txt";

        public string Awards { get; } = $@"{Main}\Awards.txt";

        public static readonly char separator;

        private static readonly string messageNotFound;

        static FileStorage()
        {
            Disk = "D";
            Tom = $@"{Disk}:\";
            Main = $"{Tom}Task06";
            Root = $@"{Main}\Storage";
            separator = '|';
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

            if (!File.Exists(Users))
            {
                Thread.Sleep(10);
                var streamWriter = new StreamWriter(Users, true);

                streamWriter.Write("");
                streamWriter.Close();
            }

            if (!File.Exists(Awards))
            {
                Thread.Sleep(10);
                var streamWriter = new StreamWriter(Awards, true);

                streamWriter.Write("");
                streamWriter.Close();
            }
        }

        public void PrintStoragePaths()
        {
            Console.WriteLine("---------Task folders--------");

            bool isFolder = true;

            PrintSinglePath(Main, "Main", isFolder);
            PrintSinglePath(Root, "Root", isFolder);

            isFolder = false;
            PrintSinglePath(Users, "Users", isFolder);
            PrintSinglePath(Awards, "Awards", isFolder);

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

        public void AddUser(User user)
        {
            PrepareFile(Users);

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(Users, true);

            streamWriter.Write(user.UserID + separator);
            streamWriter.Write(user.AwardID + separator);
            streamWriter.Write(user.Name + separator);
            streamWriter.Write(user.DateOfBirth.ToString("dd.MM.yyyy") + separator);
            streamWriter.Write(user.Age);
            streamWriter.WriteLine();

            streamWriter.Close();
        }

        private static void PrepareFile(string path)
        {
            if (!File.Exists(path))
            {
                Thread.Sleep(10);
                var streamWriter = new StreamWriter(path, true);

                streamWriter.Write("");
                streamWriter.Close();
            }
            else
            {
                Thread.Sleep(10);
                var attributes = File.GetAttributes(path);

                if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    Thread.Sleep(10);
                    File.SetAttributes(path, FileAttributes.Normal);
                }
            }
        }

        public void RemoveElement(string name, string path, string fileName)
        {
            PrepareFile(path);

            Thread.Sleep(10);
            var lines = File.ReadAllLines(path);

            File.Delete(path);

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(path, true);

            foreach (var line in lines)
            {
                if (NameInLine(line, fileName) != name)
                {
                    streamWriter.Write(line);
                    streamWriter.WriteLine();
                }
            }

            streamWriter.Close();
        }

        private static string NameInLine(string line, string fileName)
        {
            switch (fileName)
            {
                case "Users":
                    return line.Split(separator)[User.GetFieldIndex("Name")];
                default:
                    return line.Split(separator)[Award.GetFieldIndex("Title")];
            }
        }

        public void PrintFileContent(string path)
        {
            Console.WriteLine();

            PrepareFile(path);

            Thread.Sleep(10);
            var lines = File.ReadLines(path);

            foreach (var line in lines)
            {
                var charArray = line.Split('|');

                for (int i = 2; i < charArray.Length; i++)
                {
                    Console.Write(charArray[i] + "---");
                }

                Console.WriteLine();
            }
        }

        public void AddAward(Award award)
        {
            PrepareFile(Awards);

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(Awards, true);

            streamWriter.Write(award.AwardID + separator);
            streamWriter.Write(award.UserID + separator);
            streamWriter.Write(award.Title + separator);
            streamWriter.WriteLine();

            streamWriter.Close();
        }
    }
}
