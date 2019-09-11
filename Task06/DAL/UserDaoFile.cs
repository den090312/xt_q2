using Entities;
using InterfacesDAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace DAL
{
    public class UserDaoFile : IUserDao
    {
        public static string FilePath { get; private set; }

        public static string FileName { get; private set; }

        public static char Separator { get; private set; }

        static UserDaoFile()
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

            streamWriter.Write(user.UserId + Separator);
            streamWriter.Write(user.Name + Separator);
            streamWriter.Write(user.DateOfBirth.ToString("dd.MM.yyyy") + Separator);
            streamWriter.Write(user.Age);
            streamWriter.WriteLine();

            streamWriter.Close();
        }

        public void RemoveUsers(string userName)
        {
            if (File.Exists(FilePath))
            {
                SetNormalAttributes();

                Thread.Sleep(10);
                var userLines = File.ReadAllLines(FilePath);

                File.Delete(FilePath);

                Thread.Sleep(10);
                var streamWriter = new StreamWriter(FilePath, true);

                foreach (var line in userLines)
                {
                    if (Name(line) != userName)
                    {
                        streamWriter.WriteLine(line);
                    }
                }

                streamWriter.Close();
            }
        }

        public void PrintUsers()
        {
            if (File.Exists(FilePath))
            {
                SetNormalAttributes();

                Thread.Sleep(10);
                var userLines = File.ReadAllLines(FilePath);

                foreach (var line in userLines)
                {
                    var lineArray = line.Split(Separator);

                    PrintSingleUser(ref lineArray);
                    Console.WriteLine();
                }
            }
        }

        private void PrintSingleUser(ref string[] lineArray)
        {
            for (int i = 1; i < lineArray.Length; i++)
            {
                Console.Write(lineArray[i]);

                if (i != lineArray.Length - 1)
                {
                    Console.Write("---");
                }
            }
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

            streamWriter.Write(string.Empty);
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
            var fieldIndex = User.GetFieldIndex(itemName);

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

        public string[] GetUserIdArray(string userName)
        {
            var userIdList = new List<string>();

            CheckFileExistance();

            Thread.Sleep(10);
            var userLines = File.ReadAllLines(FilePath);

            foreach (var line in userLines)
            {
                if (Name(line) == userName)
                {
                    userIdList.Add(UserID(line));
                }
            }

            return userIdList.ToArray();
        }

        public string[] GetAllUsers()
        {
            CheckFileExistance();

            Thread.Sleep(10);

            return File.ReadAllLines(FilePath);
        }

        private void CheckFileExistance()
        {
            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException($"{nameof(FilePath)} is not exists!");
            }
        }
    }
}
