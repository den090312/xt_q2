﻿using Entities;
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

        public bool JoinAwardToUser(User user, Award award)
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

        public IEnumerable<Award> GetAwardsByUserGuid(Guid userGuid, IEnumerable<Award> awards)
        {
            if (!File.Exists(FilePath))
            {
                return new List<Award>();
            }

            return GetAwards(userGuid, awards);
        }

        private IEnumerable<Award> GetAwards(Guid userGuid, IEnumerable<Award> awards)
        {
            var awardsByUser = new List<Award>();
            var userAwardLines = File.ReadAllLines(FilePath);

            if (userAwardLines.Length == 0)
            {
                return new List<Award>();
            }

            foreach (var line in userAwardLines)
            {
                if (line == string.Empty)
                {
                    continue;
                }

                var guidUser = line.Split(Separator)[0];
                var awardGuid = line.Split(Separator)[1];

                if (userGuid.ToString() != guidUser)
                {
                    continue;
                }

                var title = GetAwardTitle(awards, awardGuid);

                if (title == string.Empty)
                {
                    continue;
                }

                else
                {
                    var guid = Guid.Parse(awardGuid);

                    awardsByUser.Add(new Award(guid, title));
                }
            }

            return awardsByUser;
        }

        private void Join(User user, Award award)
        {
            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);

            streamWriter.WriteLine(user.Guid.ToString() + Separator + award.Guid.ToString());
            streamWriter.Close();
        }

        private IEnumerable<UserAward> GetAll(IEnumerable<User> users, IEnumerable<Award> awards)
        {
            var usersAwards = new List<UserAward>();

            if (!File.Exists(FilePath))
            {
                return usersAwards;
            }

            foreach (var user in users)
            {
                usersAwards = GetUserAwards(user, awards, usersAwards);
            }

            return usersAwards;
        }

        private List<UserAward> GetUserAwards(User user, IEnumerable<Award> awards, List<UserAward> usersAwards)
        {
            var awardsByUser = GetAwardsByUserGuid(user.Guid, awards);

            foreach (var award in awardsByUser)
            {
                usersAwards.Add(new UserAward(user, award));
            }

            return usersAwards;
        }

        private string GetAwardTitle(IEnumerable<Award> awards, string awardId)
        {
            foreach (var award in awards)
            {
                if (award.Guid.ToString() == awardId)
                {
                    return award.Title;
                }
            }

            return string.Empty;
        }

        public bool RemoveUserAwards(Guid userGuid, IEnumerable<User> users, IEnumerable<Award> awards)
        {
            if (!File.Exists(FilePath))
            {
                return false;
            }

            try
            {
                UserAwardsRemove(userGuid, users, awards);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetInfo() => FilePath;

        private void UserAwardsRemove(Guid userGuid, IEnumerable<User> users, IEnumerable<Award> awards)
        {
            var usersAwards = GetAll(users, awards);

            if (!File.Exists(FilePath))
            {
                return;
            }

            File.Delete(FilePath);

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);

            foreach (var userAward in usersAwards)
            {
                if (userAward.UserRef.Guid != userGuid)
                {
                    PrintLine(streamWriter, userAward);
                }
            }

            streamWriter.Close();
        }

        private void PrintLine(StreamWriter streamWriter, UserAward userAward)
        {
            streamWriter.WriteLine(userAward.UserRef.Guid.ToString() + Separator + userAward.AwardRef.Guid.ToString());
            streamWriter.WriteLine();
        }

        public bool RemoveAwardUsers(Guid awardGuid, IEnumerable<User> users, IEnumerable<Award> awards)
        {
            if (!File.Exists(FilePath))
            {
                return false;
            }

            try
            {
                AwardUsersRemove(awardGuid, users, awards);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void AwardUsersRemove(Guid awardGuid, IEnumerable<User> users, IEnumerable<Award> awards)
        {
            var usersAwards = GetAll(users, awards);

            if (!File.Exists(FilePath))
            {
                return;
            }

            File.Delete(FilePath);

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);

            foreach (var userAward in usersAwards)
            {
                if (userAward.AwardRef.Guid != awardGuid)
                {
                    PrintLine(streamWriter, userAward);
                }
            }

            streamWriter.Close();
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

            if (!File.Exists(FilePath))
            {
                return;
            }

            var attributes = File.GetAttributes(FilePath);

            if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                Thread.Sleep(10);
                File.SetAttributes(FilePath, FileAttributes.Normal);
            }
        }

        public List<KeyValuePair<Guid, Guid>> GetGuidPairs()
        {
            throw new NotImplementedException();
        }
    }
}