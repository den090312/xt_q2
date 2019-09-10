using Entities;
using InterfacesDAL;
using System;
using System.IO;
using System.Threading;

namespace DAL
{
    public class UserDFO : IUserDFO
    {
        public static string FilePath { get; private set; }

        public static string FileName { get; private set; }

        public static char Separator { get; private set; }

        static UserDFO()
        {
            FilePath = @"D:\Task06\Users.txt";
            FileName = "Users.txt";
            Separator = '|';
        }

        public void AddUser(User user)
        {
            PrepareUserFile();

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);

            streamWriter.Write(user.UserID + Separator);
            streamWriter.Write(user.Name + Separator);
            streamWriter.Write(user.DateOfBirth.ToString("dd.MM.yyyy") + Separator);
            streamWriter.Write(user.Age);
            streamWriter.WriteLine();

            streamWriter.Close();
        }

        public void EraseUser(string userID)
        {
            if (File.Exists(FilePath))
            {
                Thread.Sleep(10);
                var userLines = File.ReadAllLines(FilePath);

                File.Delete(FilePath);

                Thread.Sleep(10);
                var streamWriter = new StreamWriter(FilePath, true);

                foreach (var line in userLines)
                {
                    if (UserID(line) != userID)
                    {
                        streamWriter.Write(line);
                        streamWriter.WriteLine();
                    }
                }

                streamWriter.Close();
            }
        }

        public void PrintUsers()
        {
            PrepareUserFile();

            Thread.Sleep(10);
            var userLines = File.ReadAllLines(FilePath);

            foreach (var line in userLines)
            {
                var lineArray = line.Split(Separator);

                PrintSingleUser(ref lineArray);
            }
        }

        private void PrintSingleUser(ref string[] lineArray)
        {
            for (int i = 2; i < lineArray.Length; i++)
            {
                Console.Write(lineArray[i]);

                if (i != lineArray.Length - 1)
                {
                    Console.Write("---");
                }
            }
        }

        public void RemoveUsers(string userName)
        {
            PrepareUserFile();

            Thread.Sleep(10);
            var userLines = File.ReadAllLines(FilePath);

            File.Delete(FilePath);

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);

            foreach (var line in userLines)
            {
                if (Name(line) != userName)
                {
                    streamWriter.Write(line);
                    streamWriter.WriteLine();
                }
            }

            streamWriter.Close();
        }

        public string[] GetUserIDArray(string userName)
        {
            throw new System.NotImplementedException();
        }

        public bool UsersExists(string userName)
        {
            throw new System.NotImplementedException();
        }

        private void PrepareUserFile()
        {
            if (File.Exists(FilePath))
            {
                SetNormalAttributes();
            }
            else
            {
                CreateUserFile();
            }
        }

        private void CreateUserFile()
        {
            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);

            streamWriter.Write("");
            streamWriter.Close();
        }

        private void SetNormalAttributes()
        {
            Thread.Sleep(10);
            var attributes = File.GetAttributes(FilePath);

            if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                Thread.Sleep(10);
                File.SetAttributes(FilePath, FileAttributes.Normal);
            }
        }

        private static string UserID(string line) => GetItemInLine("UserID", line);

        private static string Name(string line) => GetItemInLine("Name", line);

        private static string GetItemInLine(string itemName, string line)
        {
            var fieldIndex = Award.GetFieldIndex(itemName);

            if (fieldIndex == -1)
            {
                throw new Exception("fieldIndex is not found!");
            }

            switch (fieldIndex)
            {
                case -1:
                    return string.Empty;
                default:
                    return line.Split(Separator)[fieldIndex];
            }
        }
    }
}
