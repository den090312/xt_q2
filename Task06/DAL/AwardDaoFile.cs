﻿using Entities;
using InterfacesDAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace DAL
{
    public class AwardDaoFile : IAwardDao
    {
        public static string FilePath { get; private set; }

        public static string FileName { get; private set; }

        public static char Separator { get; private set; }

        static AwardDaoFile()
        {
            FilePath = @"D:\Task06\Awards.txt";
            FileName = "Awards.txt";
            Separator = '|';
        }

        public void AddAward(Award award)
        {
            PrepareUserFile();

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);

            streamWriter.Write(award.AwardID + Separator);
            streamWriter.Write(award.Title);
            streamWriter.WriteLine();

            streamWriter.Close();
        }

        public void RemoveAwards(string title)
        {
            if (File.Exists(FilePath))
            {
                SetNormalAttributes();

                Thread.Sleep(10);
                var awardLines = File.ReadAllLines(FilePath);

                File.Delete(FilePath);

                Thread.Sleep(10);
                var streamWriter = new StreamWriter(FilePath, true);

                foreach (var line in awardLines)
                {
                    if (Title(line) != title)
                    {
                        streamWriter.WriteLine(line);
                    }
                }

                streamWriter.Close();
            }
        }

        public void PrintAwards()
        {
            if (File.Exists(FilePath))
            {
                SetNormalAttributes();

                Thread.Sleep(10);
                var awardLines = File.ReadAllLines(FilePath);

                foreach (var line in awardLines)
                {
                    var lineArray = line.Split(Separator);

                    PrintSingleAward(ref lineArray);
                    Console.WriteLine();
                }
            }
        }

        private void PrintSingleAward(ref string[] lineArray)
        {
            for (int i = 1; i < lineArray.Length; i++)
            {
                Console.Write(lineArray[i]);

                if (i != lineArray.Length - 1)
                {
                    Console.Write("---");
                }
            }
        }

        private void PrepareUserFile()
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

        private static string AwardID(string line) => GetItemInLine("AwardID", line);

        private static string Title(string line) => GetItemInLine("Title", line);

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

        public string[] GetAwardIdArray(string awardName)
        {
            var userIdList = new List<string>();

            CheckFileExistance();

            Thread.Sleep(10);
            var userLines = File.ReadAllLines(FilePath);

            foreach (var line in userLines)
            {
                if (Title(line) == awardName)
                {
                    userIdList.Add(AwardID(line));
                }
            }

            return userIdList.ToArray();
        }

        public string[] GetAllAwards()
        {
            CheckFileExistance();

            Thread.Sleep(10);

            return File.ReadAllLines(FilePath);
        }

        private void CheckFileExistance()
        {
            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException($"{nameof(FilePath)} is not exists!");
            }
        }
    }
}