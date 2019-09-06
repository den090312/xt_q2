using Task06.Entities;
using Task06.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Task06.DAL
{
    public class UserFileStorage : IUserable
    {
        private readonly string messageNotFound = "not found!";

        public static string FilePath { get; private set; }

        public static string FileName{ get; private set; }

        public static char Separator { get; private set; }

        static UserFileStorage()
        {
            FilePath = @"D:\Task06\Users.txt";
            FileName = "Users.txt";
            Separator = '|';
        }

        public void CreateStorage()
        {
            if (!File.Exists(FilePath))
            {
                Thread.Sleep(10);
                var streamWriter = new StreamWriter(FilePath, true);

                streamWriter.Write("");
                streamWriter.Close();
            }
        }

        public void PrintStorageInfo()
        {
            if (File.Exists(FilePath))
            {
                Console.WriteLine($"{FileName} - {FilePath}");
            }
            else
            {
                Console.WriteLine($"{FileName} - {messageNotFound}");
            }
        }

        public User CreateUser(string name, DateTime dateBirth) => new User(name, dateBirth);

        public void AddUser(User user)
        {
            PrepareFile();

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);

            streamWriter.Write(user.UserID + Separator);
            streamWriter.Write(user.AwardID + Separator);
            streamWriter.Write(user.Name + Separator);
            streamWriter.Write(user.DateOfBirth.ToString("dd.MM.yyyy") + Separator);
            streamWriter.Write(user.Age);
            streamWriter.WriteLine();

            streamWriter.Close();
        }

        public void RemoveUsers(string userName)
        {
            PrepareFile();

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

        public bool UsersExists(string userName)
        {
            bool exists = false;

            CheckFileExistance();
            SetNormalAttributes();

            Thread.Sleep(10);
            var userLines = File.ReadAllLines(FilePath);

            Thread.Sleep(10);
            using (var streamWriter = new StreamWriter(FilePath, true))
            {
                foreach (var line in userLines)
                {
                    if (Name(line) == userName)
                    {
                        streamWriter.Close();

                        return true;
                    }
                }

                streamWriter.Close();
            }

            return exists;
        }

        public void PrintUsers(List<KeyValuePair<string, string>> awardList)
        {
            PrepareFile();

            Thread.Sleep(10);
            var userLines = File.ReadAllLines(FilePath);

            var currentUserID = string.Empty;

            foreach (var line in userLines)
            {
                var lineArray = line.Split(Separator);

                if (currentUserID != lineArray[0])
                {
                    PrintUser(ref lineArray);
                    PrintAwards(ref awardList, UserID(lineArray[0]));
                }

                currentUserID = lineArray[0];
            }
        }

        private static void PrintUser(ref string[] lineArray)
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

        private static void PrintAwards(ref List<KeyValuePair<string, string>> awardList, string userID)
        {
            Console.WriteLine();

            var selectedList = awardList.FindAll(x => x.Key == userID);

            foreach (var kvPair in selectedList)
            {
                Console.WriteLine($"------{kvPair.Value}");
            }
        }

        public void JoinAwardToUser(string awardID, string userID)
        {
            CheckFileExistance();
            SetNormalAttributes();

            Thread.Sleep(10);
            var userLines = File.ReadAllLines(FilePath);

            File.Delete(FilePath);

            Thread.Sleep(10);
            var sw = new StreamWriter(FilePath, true);
            var recorded = false;

            foreach (var userLine in userLines)
            {
                recorded = RunRecord(ref awardID, ref userID, ref sw, recorded, userLine);
                sw.WriteLine();
            }

            sw.Close();
        }

        private static bool RunRecord(ref string awardID, ref string userID, ref StreamWriter sw, bool recorded, string userLine)
        {
            if (UserID(userLine) == userID)
            {
                if (AwardID(userLine) == string.Empty)
                {
                    sw.Write(NewLine(userLine, awardID));
                }
                else
                {
                    sw.Write(userLine);

                    if (!recorded)
                    {
                        sw.WriteLine();
                        sw.Write(NewLine(userLine, awardID));
                    }

                    recorded = true;
                }
            }
            else
            {
                sw.Write(userLine);
            }

            return recorded;
        }

        private static string Name(string line) => GetItemInLine("Name", line);

        private static string UserID(string line) => GetItemInLine("UserID", line);

        private static string AwardID(string line) => GetItemInLine("AwardID", line);

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

        private static string NewLine(string line, string id)
        {
            int indexID = User.GetFieldIndex("AwardID");

            if (indexID == -1)
            {
                throw new Exception("indexID is not found!");
            }

            var lineArray = line.Split(Separator);
            var sb = new StringBuilder();

            FillLine(ref id, lineArray, ref sb, ref indexID);

            return sb.ToString();
        }

        private static void FillLine(ref string id, string[] lineArray, ref StringBuilder sb, ref int indexID)
        {
            for (int i = 0; i < lineArray.Length; i++)
            {
                if (i == indexID)
                {
                    sb.Append(id);

                    if (i != lineArray.Length - 1)
                    {
                        sb.Append(Separator);
                    }
                }
                else
                {
                    sb.Append(lineArray[i]);

                    if (i != lineArray.Length - 1)
                    {
                        sb.Append(Separator);
                    }
                }
            }
        }

        private void PrepareFile()
        {
            if (!File.Exists(FilePath))
            {
                Thread.Sleep(10);
                var streamWriter = new StreamWriter(FilePath, true);

                streamWriter.Write("");
                streamWriter.Close();
            }
            else
            {
                SetNormalAttributes();
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

        private void CheckFileExistance()
        {
            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException($"{nameof(FilePath)} is not exists!");
            }
        }

        public string[] GetUserIDArray(string userName)
        {
            var userIDList = new List<string>();

            CheckFileExistance();

            Thread.Sleep(10);
            var userLines = File.ReadAllLines(FilePath);

            foreach (var line in userLines)
            {
                if (Name(line) == userName)
                {
                    userIDList.Add(UserID(line));
                }
            }

            return userIDList.ToArray();
        }

        public bool RecordExists(string awardID, string userID)
        {
            bool recordExists = false;

            PrepareFile();

            Thread.Sleep(10);
            var userLines = File.ReadAllLines(FilePath);

            foreach (var line in userLines)
            {
                if (AwardID(line) == awardID & UserID(line) == userID)
                {
                    return true;
                }
            }

            return recordExists;
        }

        public void EraseAward(string awardID)
        {
            PrepareFile();

            Thread.Sleep(10);
            var userLines = File.ReadAllLines(FilePath);

            File.Delete(FilePath);

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);

            foreach (var line in userLines)
            {
                if (AwardID(line) != awardID)
                {
                    streamWriter.Write(line);
                    streamWriter.WriteLine();
                }
            }

            streamWriter.Close();
        }
    }
}
