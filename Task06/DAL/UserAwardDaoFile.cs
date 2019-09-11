using InterfacesDAL;
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

        public void JoinAwardsToUsers(string[] userIdArray, string[] awardIdArray)
        {
            PrepareUserAwardFile();

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);

            foreach (var userId in userIdArray)
            {
                foreach (var awardId in awardIdArray)
                {
                    streamWriter.WriteLine(userId + Separator + awardId);
                }
            }

            streamWriter.Close();
        }

        public void PrintUsersAwards()
        {
            var userAwardsDict = GetUserAwardsDict();
        }

        private Dictionary<string, string[]> GetUserAwardsDict()
        {
            var userAwardsDict = new Dictionary<string, string[]>();

            if (File.Exists(FilePath))
            {
                SetNormalAttributes();

                Thread.Sleep(10);
                var userAwardLines = File.ReadAllLines(FilePath);

                foreach (var userAwardLine in userAwardLines)
                {
                    var userId = userAwardLine.Split(Separator)[0];
                    var awardIdArray = GetAwardIdArray(userAwardLines, userId);

                    userAwardsDict.Add(userId, awardIdArray);
                }
            }

            return userAwardsDict;
        }

        private string[] GetAwardIdArray(string[] userAwardLines, string userId)
        {
            var awardIdList = new List<string>();

            foreach (var userAwardLine in userAwardLines)
            {
                var charArray = userAwardLine.Split(Separator);

                if (charArray[0] == userId)
                {
                    awardIdList.Add(charArray[1]);
                }
            }

            return awardIdList.ToArray();
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