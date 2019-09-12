using Entities;
using InterfacesDAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public bool JoinedAwardToUser(Guid userGuid, Guid awardGuid)
        {
            PrepareUserAwardFile();

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);

            //ToDo

            streamWriter.Close();

            return true;
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
            throw new NotImplementedException();
        }

        public bool AwardRemoved(Guid awardGuid)
        {
            throw new NotImplementedException();
        }

        private void PrepareUserAwardFile()
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