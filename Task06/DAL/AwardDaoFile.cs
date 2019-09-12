using Entities;
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

        public bool AwardAdded(Award award)
        {
            PrepareAwardFile();

            try
            {
                AddAward(award);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void AddAward(Award award)
        {
            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);

            PrintLine(streamWriter, award);

            streamWriter.Close();
        }

        public bool AwardRemoved(Guid awardGuid)
        {
            if (!File.Exists(FilePath))
            {
                return false;
            }

            try
            {
                RemoveAward(awardGuid);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void RemoveAward(Guid awardGuid)
        {
            var users = GetAll();

            File.Delete(FilePath);

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);

            foreach (var award in users)
            {
                if (award.AwardGuid != awardGuid)
                {
                    PrintLine(streamWriter, award);
                }
            }

            streamWriter.Close();
        }

        private void PrintLine(StreamWriter streamWriter, Award award)
        {
            streamWriter.Write(award.AwardGuid.ToString() + Separator);
            streamWriter.Write(award.Title);
            streamWriter.WriteLine();
        }

        public IEnumerable<Award> GetAll()
        {
            throw new NotImplementedException();
        }

        public Award GetAwardByGuid(Guid awardGuid)
        {
            throw new NotImplementedException();
        }

        private void PrepareAwardFile()
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

        private void CheckFileExistance()
        {
            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException($"{nameof(FilePath)} is not exists!");
            }
        }
    }
}