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

        public static string Users { get; } 

        private static string Awards { get; }

        public static readonly char separator;

        private static readonly string messageNotFound;

        static FileStorage()
        {
            Disk = "D";
            Tom = $@"{Disk}:\";
            Main = $"{Tom}Task06";
            Root = $@"{Main}\Storage";
            Users = $@"{Main}\Users.txt";
            Awards = $@"{Main}\Awards.txt";
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
            PrepareUsersFile();

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

        private static void PrepareUsersFile()
        {
            if (!File.Exists(Users))
            {
                Thread.Sleep(10);
                var streamWriter = new StreamWriter(Users, true);

                streamWriter.Write("");
                streamWriter.Close();
            }
            else
            {
                Thread.Sleep(10);
                var attributes = File.GetAttributes(Users);

                if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    Thread.Sleep(10);
                    File.SetAttributes(Users, FileAttributes.Normal);
                }
            }
        }

        public void RemoveUser(string name)
        {
            PrepareUsersFile();

            Thread.Sleep(10);
            var lines = File.ReadAllLines(Users);

            File.Delete(Users);

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(Users, true);

            foreach (var line in lines)
            {
                if (NameInLine(line) != name)
                {
                    streamWriter.Write(line);
                    streamWriter.WriteLine();
                }
            }

            streamWriter.Close();
        }

        private static string NameInLine(string line) => line.Split(separator)[User.GetFieldIndex("Name")];

        public void PrintAllUsers()
        {
            Console.WriteLine();

            PrepareUsersFile();

            Thread.Sleep(10);
            var lines = File.ReadLines(Users);

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

        }
    }
}
