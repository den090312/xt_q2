using _61_62_USERS_AND_AWARDS.Entities;
using _61_62_USERS_AND_AWARDS.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace _61_62_USERS_AND_AWARDS.DAL
{
    public class AwardFileStrorage : IAwardable
    {
        private readonly string messageNotFound = "not found!";

        public static string FilePath { get; private set; }

        public static string FileName { get; private set; }

        public static char Separator { get; private set; }

        static AwardFileStrorage()
        {
            FilePath = $@"D:\Task06\Awards.txt";
            FileName = "Awards.txt";
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

        public void AddAward(Award award)
        {
            PrepareFile();

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);

            streamWriter.Write(award.AwardID + Separator);
            streamWriter.Write(award.UserID + Separator);
            streamWriter.Write(award.Title);
            streamWriter.WriteLine();

            streamWriter.Close();
        }

        public bool AwardExists(string awardName)
        {
            bool exists = false;

            CheckFileExistance();
            SetNormalAttributes();

            Thread.Sleep(10);
            var lines = File.ReadAllLines(FilePath);

            Thread.Sleep(10);
            using (var streamWriter = new StreamWriter(FilePath, true))
            {
                foreach (var line in lines)
                {
                    if (NameInLIne(line) == awardName)
                    {
                        streamWriter.Close();

                        return true;
                    }
                }

                streamWriter.Close();
            }

            return exists;
        }

        public void RemoveAward(string awardName)
        {
            PrepareFile();

            Thread.Sleep(10);
            var lines = File.ReadAllLines(FilePath);

            File.Delete(FilePath);

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);

            foreach (var line in lines)
            {
                if (NameInLIne(line) != awardName)
                {
                    streamWriter.Write(line);
                    streamWriter.WriteLine();
                }
            }

            streamWriter.Close();
        }

        public void PrintAwards()
        {
            PrepareFile();

            Thread.Sleep(10);
            var lines = File.ReadLines(FilePath);

            var currentUserID = string.Empty;

            foreach (var line in lines)
            {
                var lineArray = line.Split(Separator);

                if (currentUserID != lineArray[0])
                {
                    PrintAward(ref lineArray);

                    Console.WriteLine();
                }

                currentUserID = lineArray[0];
            }
        }

        private static void PrintAward(ref string[] lineArray)
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

        public void AddUserToAward(string userID, string awardID)
        {
            CheckFileExistance();
            SetNormalAttributes();

            Thread.Sleep(10);
            var awardLines = File.ReadAllLines(FilePath);

            File.Delete(FilePath);

            Thread.Sleep(10);
            var sw = new StreamWriter(FilePath, true);
            var recorded = false;

            foreach (var awardLine in awardLines)
            {
                RunRecord(ref userID, ref awardID, ref sw, recorded, awardLine);
                sw.WriteLine();
            }

            sw.Close();
        }

        private static void RunRecord(ref string userID, ref string awardID, ref StreamWriter sw, bool recorded, string awardLine)
        {
            if (AwardID(awardLine) == awardID)
            {
                if (UserID(awardLine) == string.Empty)
                {
                    sw.Write(NewLine(awardLine, userID));
                }
                else
                {
                    sw.Write(awardLine);

                    if (!recorded)
                    {
                        sw.WriteLine();
                        sw.Write(NewLine(awardLine, userID));
                    }

                    recorded = true;
                }
            }
            else
            {
                sw.Write(awardLine);
            }
        }

        private static string NameInLIne(string line) => line.Split(Separator)[Award.GetFieldIndex("Title")];

        private static string AwardID(string line) => line.Split(Separator)[Award.GetFieldIndex("AwardID")];

        private static string UserID(string line) => line.Split(Separator)[Award.GetFieldIndex("UserID")];

        private static string NewLine(string line, string id)
        {
            int indexID = Award.GetFieldIndex("UserID");

            if (indexID == -1)
            {
                throw new Exception("indexID is not found!");
            }

            var itemArray = line.Split(Separator);
            var sb = new StringBuilder();

            FillLine(ref id, itemArray, ref sb, ref indexID);

            return sb.ToString();
        }

        private static void FillLine(ref string id, string[] itemArray, ref StringBuilder sb, ref int indexID)
        {
            for (int i = 0; i < itemArray.Length; i++)
            {
                if (i == indexID)
                {
                    sb.Append(id);

                    if (i != itemArray.Length - 1)
                    {
                        sb.Append(Separator);
                    }
                }
                else
                {
                    sb.Append(itemArray[i]);

                    if (i != itemArray.Length - 1)
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

        public void CheckFileExistance()
        {
            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException($"{nameof(FilePath)} is not exists!");
            }
        }

        public string GetID(string awardName)
        {
            var awardID = string.Empty;

            CheckFileExistance();

            Thread.Sleep(10);
            var awardLines = File.ReadAllLines(FilePath);

            foreach (var awardLine in awardLines)
            {
                if (NameInLIne(awardLine) == awardName)
                {
                    return awardLine.Split(Separator)[Award.GetFieldIndex("AwardID")];
                }
            }

            return awardID;
        }

        public List<KeyValuePair<string, string>> GetAwards()
        {
            CheckFileExistance();

            var awardsDict = new List<KeyValuePair<string, string>>();

            Thread.Sleep(10);
            var awardLines = File.ReadAllLines(FilePath);

            foreach (var awardLine in awardLines)
            {
                awardsDict.Add(new KeyValuePair<string, string>(UserID(awardLine), NameInLIne(awardLine)));
            }

            return awardsDict;
        }
    }
}
