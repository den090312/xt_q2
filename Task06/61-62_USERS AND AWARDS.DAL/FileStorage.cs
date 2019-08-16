using _61_62_USERS_AND_AWARDS.Entities;
using _61_62_USERS_AND_AWARDS.Interfaces;
using System;
using System.IO;
using System.Text;
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

        public void RemoveElement(string elementName, string filePath, string fileName)
        {
            PrepareFile(filePath);

            Thread.Sleep(10);
            var lines = File.ReadAllLines(filePath);

            File.Delete(filePath);

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(filePath, true);

            foreach (var line in lines)
            {
                if (NameInLine(line, fileName) != elementName)
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
            PrepareFile(path);

            Thread.Sleep(10);
            var lines = File.ReadLines(path);

            foreach (var line in lines)
            {
                var charArray = line.Split('|');

                for (int i = 2; i < charArray.Length; i++)
                {
                    Console.Write(charArray[i]);

                    if (i != charArray.Length - 1)
                    {
                        Console.Write("---");
                    }
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
            streamWriter.Write(award.Title);
            streamWriter.WriteLine();

            streamWriter.Close();
        }

        public void AddAwardToUser(string user, string award)
        {
            CheckFileExistance(Users);

            Thread.Sleep(10);
            var lines = File.ReadAllLines(Users);

            File.Delete(Users);

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(Users, true);

            foreach (var line in lines)
            {
                if (NameInLine(line, Users) != user)
                {
                    streamWriter.Write(line);
                    streamWriter.WriteLine();
                }
                else
                {
                    var id = GetAwardID(award);

                    if (id != string.Empty)
                    {
                        streamWriter.Write(LineWithID(line, id, "Users"));
                    }
                }
            }

            streamWriter.Close();

            AddUserToAward(award, user);
        }

        public void AddUserToAward(string award, string user)
        {
            CheckFileExistance(Awards);

            Thread.Sleep(10);
            var lines = File.ReadAllLines(Awards);

            File.Delete(Awards);

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(Awards, true);

            foreach (var line in lines)
            {
                if (NameInLine(line, Awards) != award)
                {
                    streamWriter.Write(line);
                    streamWriter.WriteLine();
                }
                else
                {
                    var id = GetUserID(user);

                    if (id != string.Empty)
                    {
                        streamWriter.Write(LineWithID(line, id, "Awards"));
                    }
                }
            }
        }

        public static string LineWithID(string line, string id, string fileName)
        {
            var itemArray = line.Split('|');
            var sb = new StringBuilder();

            int indexID;

            switch (fileName)
            {
                case "Users":
                    indexID = User.GetFieldIndex("AwardID");
                    break;
                default:
                    indexID = Award.GetFieldIndex("UserID");
                    break;
            }

            if (indexID == -1)
            {
                throw new Exception("indexID is not found!");
            }

            for (int i = 0; i < itemArray.Length; i++)
            {
                if (i == indexID)
                {
                    sb.Append(id);

                    if (i != itemArray.Length - 1)
                    {
                        sb.Append(separator);
                    }
                }
                else
                {
                    sb.Append(itemArray[i]);

                    if (i != itemArray.Length - 1)
                    {
                        sb.Append(separator);
                    }
                }
            }

            return sb.ToString();
        }

        public string GetAwardID(string award)
        {
            string guid = string.Empty;

            Thread.Sleep(10);
            var lines = File.ReadAllLines(Users);

            foreach (var line in lines)
            {
                if (NameInLine(line, Awards) != award)
                {
                    return line.Split(separator)[Award.GetFieldIndex("AwardID")];
                }
            }

            return guid;
        }

        public string GetUserID(string user)
        {
            string guid = string.Empty;

            Thread.Sleep(10);
            var lines = File.ReadAllLines(Awards);

            foreach (var line in lines)
            {
                if (NameInLine(line, Users) != user)
                {
                    return line.Split(separator)[User.GetFieldIndex("UserID")];
                }
            }

            return guid;
        }

        public bool ElementExists(string elementName, string filePath, string fileName)
        {
            bool exists = false;

            PrepareFile(filePath);

            Thread.Sleep(10);
            var lines = File.ReadAllLines(filePath);

            File.Delete(filePath);

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(filePath, true);

            foreach (var line in lines)
            {
                if (NameInLine(line, fileName) == elementName)
                {
                    return true;
                }
            }

            streamWriter.Close();

            return exists;
        }

        public static void CheckFileExistance(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"{nameof(filePath)} is not exists!");
            }
        }
    }
}
