using Entities;
using InterfacesDAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace DAL
{
    public class UserAwardDaoFile : IUserAwardDao
    {
        public static string FilePath { get; private set; }

        public static string FileName { get; private set; }

        public static char Separator { get; private set; }

        static UserAwardDaoFile()
        {
            FilePath = @"C:\Task06\UsersAwards.txt";
            FileName = "UsersAwards.txt";
            Separator = '|';
        }

        public bool JoinedAwardToUser(User user, Award award)
        {
            PrepareFile();

            try
            {
                Join(user, award);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Join(User user, Award award)
        {
            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);

            streamWriter.WriteLine(user.UserGuid.ToString() + Separator + award.AwardGuid.ToString());
            streamWriter.Close();
        }

        public IEnumerable<UserAward> GetAll(IEnumerable<User> users, IEnumerable<Award> awards)
        {
            var usersAwards = new List<UserAward>();

            /*if (!File.Exists(FilePath))
            {
                return usersAwards;
            }

            var lines = File.ReadAllLines(FilePath);

            foreach (var line in lines)
            {
                var lineArray = line.Split(Separator);

                var date = DateTime.ParseExact(lineArray[2], User.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
                var user = new User(Guid.Parse(lineArray[0]), lineArray[1], date);

                usersAwards.Add(user);
            }*/

            return usersAwards;
        }

        public IEnumerable<Award> GetAwardsByUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool UserRemoved(Guid userGuid, IEnumerable<User> users, IEnumerable<Award> awards)
        {
            if (!File.Exists(FilePath))
            {
                return false;
            }

            try
            {
                RemoveUser(userGuid, users, awards);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public void PrintInfo() => Console.WriteLine(FilePath);

        private void RemoveUser(Guid userGuid, IEnumerable<User> users, IEnumerable<Award> awards)
        {
            var usersAwards = GetAll(users, awards);

            File.Delete(FilePath);

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);

            foreach (var userAward in usersAwards)
            {
                if (userAward.UserRef.UserGuid != userGuid)
                {
                    PrintLine(streamWriter, userAward);
                }
            }

            streamWriter.Close();
        }

        private void PrintLine(StreamWriter streamWriter, UserAward userAward)
        {
            streamWriter.WriteLine(userAward.UserRef.UserGuid.ToString() + Separator + userAward.AwardRef.AwardGuid.ToString());
            streamWriter.WriteLine();
        }

        public bool AwardRemoved(Guid awardGuid)
        {
            throw new NotImplementedException();
        }

        private void PrepareFile()
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
    }
}