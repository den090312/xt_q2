using Entities;
using InterfacesDAL;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            PrepareFile();

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

        public bool UserRemoved(Guid userGuid)
        {
            if (!File.Exists(FilePath))
            {
                return false;
            }

            try
            {
                RemoveUser(userGuid);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void RemoveUser(Guid userGuid)
        {
            var users = GetAll();

            File.Delete(FilePath);

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);

            foreach (var user in users)
            {
                if (user.UserGuid != userGuid)
                {
                    PrintLine(streamWriter, user);
                }
            }

            streamWriter.Close();
        }

        private void PrintLine(StreamWriter streamWriter, User user)
        {
            streamWriter.Write(user.UserGuid.ToString() + Separator);
            streamWriter.Write(user.Name + Separator);
            streamWriter.Write(user.DateOfBirth.ToString("dd.MM.yyyy") + Separator);
            streamWriter.Write(user.Age);
            streamWriter.WriteLine();
        }

        private void AddUser(User user)
        {
            PrepareFile();

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);

            PrintLine(streamWriter, user);

            streamWriter.Close();
        }

        public IEnumerable<User> GetAll()
        {
            PrepareFile();

            var userLines = File.ReadAllLines(FilePath);
            var users = new List<User>();

            foreach (var userLine in userLines)
            {
                if (userLine == string.Empty)
                {
                    continue;
                }

                AddToUsers(ref users, userLine);
            }

            return users;
        }

        private void AddToUsers(ref List<User> users, string userLine)
        {
            var userLineArray = userLine.Split(Separator);

            var date = DateTime.ParseExact(userLineArray[2], User.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
            var user = new User(Guid.Parse(userLineArray[0]), userLineArray[1], date);

            users.Add(user);
        }

        public User GetUserByGuid(Guid userGuid)
        {
            CheckFileExistence();

            var userLines = File.ReadAllLines(FilePath);

            foreach (var userLine in userLines)
            {
                if (userLine == string.Empty)
                {
                    continue;
                }

                var userLineArray = userLine.Split(Separator);

                if (userLineArray[0] == userGuid.ToString())
                {
                    var date = DateTime.ParseExact(userLineArray[2], User.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);

                    return new User(userGuid, userLineArray[1], date);
                }
            }

            return null;
        }

        private void CheckFileExistence()
        {
            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException($"{nameof(FilePath)} is not exists!");
            }
        }

        public void PrintInfo() => Console.WriteLine(FilePath);

        private void PrepareFile()
        {
            if (File.Exists(FilePath))
            {
                SetNormalAttributes();
            }
            else
            {
                File.Create(FilePath).Close();
            }
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
    }
}