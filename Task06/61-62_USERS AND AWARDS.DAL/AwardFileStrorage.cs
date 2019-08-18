using _61_62_USERS_AND_AWARDS.Entities;
using _61_62_USERS_AND_AWARDS.Interfaces;
using System;
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
                    if (Name(line) == awardName)
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
                if (Name(line) != awardName)
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

            foreach (var line in lines)
            {
                var charArray = line.Split('|');

                for (int i = 2; i < charArray.Length; i++)
                {
                    Console.Write(charArray[i]);

                    if (i != charArray.Length - 1)
                    {
                        Console.Write("---");
                    }
                }

                Console.WriteLine();
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
            var streamWriter = new StreamWriter(FilePath, true);

            foreach (var awardLine in awardLines)
            {
                if (AwardID(awardLine) == awardID)
                {
                    streamWriter.Write(LineWithID(awardLine, userID));
                }
                else
                {
                    streamWriter.Write(awardLine);
                }

                streamWriter.WriteLine();
            }

            streamWriter.Close();
        }

        private static string Name(string line) => line.Split(Separator)[Award.GetFieldIndex("Title")];

        private static string AwardID(string line) => line.Split(Separator)[Award.GetFieldIndex("AwardID")];

        private static string LineWithID(string line, string id)
        {
            var itemArray = line.Split('|');
            var sb = new StringBuilder();

            int indexID = Award.GetFieldIndex("UserID");

            if (indexID == -1)
            {
                throw new Exception("indexID is not found!");
            }

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

            return sb.ToString();
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
                if (Name(awardLine) == awardName)
                {
                    return awardLine.Split(Separator)[Award.GetFieldIndex("AwardID")];
                }
            }

            return awardID;
        }
    }
}
