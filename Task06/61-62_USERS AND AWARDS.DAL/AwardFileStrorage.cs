﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Task06.Entities;
using Task06.Interfaces;

namespace Task06.DAL
{
    public class AwardFileStrorage : IAwardable
    {
        private readonly string messageNotFound = "not found!";

        public static string FilePath { get; private set; }

        public static string FileName { get; private set; }

        public static char Separator { get; private set; }

        static AwardFileStrorage()
        {
            FilePath = @"D:\Task06\Awards.txt";
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

        public Award CreateAward(string title) => new Award(title);

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

        public bool AwardsExists(string awardTitle)
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
                    if (Title(line) == awardTitle)
                    {
                        streamWriter.Close();

                        return true;
                    }
                }

                streamWriter.Close();
            }

            return exists;
        }

        public void RemoveAwards(string awardTitle)
        {
            PrepareFile();

            Thread.Sleep(10);
            var userLines = File.ReadAllLines(FilePath);

            File.Delete(FilePath);

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);

            foreach (var line in userLines)
            {
                if (Title(line) != awardTitle)
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
            var userLines = File.ReadLines(FilePath);

            var currentUserID = string.Empty;

            foreach (var line in userLines)
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

        public void Join(string userID, string awardID)
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
                recorded = RunRecord(ref userID, ref awardID, ref sw, recorded, awardLine);
                sw.WriteLine();
            }

            sw.Close();
        }

        private static bool RunRecord(ref string userID, ref string awardID, ref StreamWriter sw, bool recorded, string awardLine)
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

            return recorded;
        }

        private static string Title(string line) => GetItemInLine("Title", line);

        private static string UserID(string line) => GetItemInLine("UserID", line);

        private static string AwardID(string line) => GetItemInLine("AwardID", line);

        private static string GetItemInLine(string itemName, string line)
        {
            var fieldIndex = Award.GetFieldIndex(itemName);

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
            int indexID = Award.GetFieldIndex("UserID");

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

        public string[] GetAwardIDArray(string awardTitle)
        {
            var awardIDList = new List<string>();

            CheckFileExistance();

            Thread.Sleep(10);
            var awardLines = File.ReadAllLines(FilePath);

            foreach (var line in awardLines)
            {
                if (Title(line) == awardTitle)
                {
                    awardIDList.Add(AwardID(line));
                }
            }

            return awardIDList.ToArray();
        }

        public List<KeyValuePair<string, string>> GetAwardList()
        {
            CheckFileExistance();

            var awardList = new List<KeyValuePair<string, string>>();

            Thread.Sleep(10);
            var awardLines = File.ReadAllLines(FilePath);

            foreach (var line in awardLines)
            {
                awardList.Add(new KeyValuePair<string, string>(UserID(line), Title(line)));
            }

            return awardList;
        }

        public void EraseUser(string userID)
        {
            PrepareFile();

            Thread.Sleep(10);
            var awardLines = File.ReadAllLines(FilePath);

            File.Delete(FilePath);

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);

            foreach (var line in awardLines)
            {
                if (UserID(line) != userID)
                {
                    streamWriter.Write(line);
                    streamWriter.WriteLine();
                }
            }

            streamWriter.Close();
        }
    }
}
