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

        public bool UserAdded(User user)
        {
            PrepareUserFile();

            try
            {
                AddUser(user);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void AddUser(User user)
        {
            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);

            streamWriter.Write(user.UserGuid.ToString() + Separator);
            streamWriter.Write(user.Name + Separator);
            streamWriter.Write(user.DateOfBirth.ToString("dd.MM.yyyy") + Separator);
            streamWriter.Write(user.Age);
            streamWriter.WriteLine();

            streamWriter.Close();
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

        private void CheckFileExistance()
        {
            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException($"{nameof(FilePath)} is not exists!");
            }
        }

        public bool UserRemoved(Guid userGuid)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}