using _61_62_USERS_AND_AWARDS.Entities;
using _61_62_USERS_AND_AWARDS.Interfaces;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace _61_62_USERS_AND_AWARDS.DAL
{
    public class AwardFileStrorage : IStorable, IAwardable
    {
        public static string FilePath { get; private set; }

        public static string FileName { get; private set; }

        public static char Separator { get; private set; }

        private string MessageNotFound { get; } = "not found";

        static AwardFileStrorage()
        {
            FilePath = $@"{FileStorage.Root}\Awards.txt";
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
            if (Directory.Exists(FilePath))
            {
                Console.WriteLine($"{FileName} - {FilePath}");
            }
            else
            {
                Console.WriteLine($"{FileName} - {MessageNotFound}");
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
            throw new NotImplementedException();
        }

        public void RemoveAward(string id)
        {
            throw new NotImplementedException();
        }

        public void PrintAwards()
        {
            throw new NotImplementedException();
        }

        public void AddAwardToUser(string user)
        {
            CheckFileExistance();

            Thread.Sleep(10);
            var lines = File.ReadAllLines(FilePath);

            File.Delete(FilePath);

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);

            foreach (var line in lines)
            {
                if (NameInLine(line) != user)
                {
                    streamWriter.Write(line);
                    streamWriter.WriteLine();
                }
                else
                {
                    var id = line.Split(Separator)[User.GetFieldIndex("UserID")];

                    if (id != string.Empty)
                    {
                        streamWriter.Write(LineWithID(line, id, "Awards"));
                    }
                }
            }

            streamWriter.Close();
        }

        private static string NameInLine(string line) => line.Split(Separator)[Award.GetFieldIndex("Title")];

        public static string LineWithID(string line, string id, string fileName)
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
    }
}
