using Entities;
using InterfacesDAL;
using System;
using System.Collections.Generic;
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
            FilePath = @"D:\Task06\UsersAwards.txt";
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

        public IEnumerable<UserAward> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Award> GetAwardsByUser(User user)
        {
            throw new NotImplementedException();
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
            var usersAwards = GetAll();

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