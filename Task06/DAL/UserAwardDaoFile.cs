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

        public void PrintUsersAwards(string[] userLines, string[] awardLines)
        {
            var userAwardsList = GetUserAwardsList();

            foreach (var kvPair in userAwardsList)
            {
                var userId = kvPair.Key;

                var userLine = GetUserLine(userLines, userId);

                if (userLine != string.Empty)
                {
                    PrintUserLine(userLine);
                }

                PrintAwards(awardLines, kvPair);
            }
        }

        private void PrintAwards(string[] awardLines, KeyValuePair<string, string[]> kvPair)
        {
            foreach (var awardLine in awardLines)
            {
                var awardIdArray = kvPair.Value;
                var awardId = awardLine.Split(Separator)[0];
                var awardName = awardLine.Split(Separator)[1];

                if (awardIdArray.Contains(awardId))
                {
                    Console.Write("---" + awardName + "---");
                }

                Console.WriteLine();
            }
        }

        private void PrintUserLine(string userLine)
        {
            for (int i = 1; i < userLine.Length; i++)
            {
                Console.Write(userLine[i]);

                if (i != userLine.Length - 1)
                {
                    Console.Write("---");
                }
            }
        }

        private string GetUserLine(string[] userLines, string userId)
        {
            foreach (var userLine in userLines)
            {
                if (userLine.Split(Separator)[0] == userId)
                {
                    return userLine;
                }
            }

            return string.Empty;
        }

        private List<KeyValuePair<string, string[]>> GetUserAwardsList()
        {
            var userAwardsList = new List<KeyValuePair<string, string[]>>();

            if (File.Exists(FilePath))
            {
                SetNormalAttributes();

                Thread.Sleep(10);
                var userAwardLines = File.ReadAllLines(FilePath);

                foreach (var userAwardLine in userAwardLines)
                {
                    var userId = userAwardLine.Split(Separator)[0];
                    var awardIdArray = GetAwardIdArray(userAwardLines, userId);

                    userAwardsList.Add(new KeyValuePair<string, string[]>(userId, awardIdArray));
                }
            }

            return userAwardsList;
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