using _61_62_USERS_AND_AWARDS.Entities;
using _61_62_USERS_AND_AWARDS.Interfaces;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace _61_62_USERS_AND_AWARDS.DAL
{
    public class UserFileStorage : IUserable
    {
        private readonly string messageNotFound = "not found!";

        public static string FilePath { get; private set; }

        public static string FileName{ get; private set; }

        public static char Separator { get; private set; }

        static UserFileStorage()
        {
            FilePath = $@"D:\Task06\Users.txt";
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

        public void RemoveUser(string userName)
        {
            PrepareFile();

            Thread.Sleep(10);
            var lines = File.ReadAllLines(FilePath);

            File.Delete(FilePath);

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);

            foreach (var line in lines)
            {
                if (Name(line) != userName)
                {
                    streamWriter.Write(line);
                    streamWriter.WriteLine();
                }
            }

            streamWriter.Close();
        }

        public bool UserExists(string userName)
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

        public void PrintUsers()
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

        public void AddAwardToUser(string awardID, string userID)
        {
            CheckFileExistance();
            SetNormalAttributes();

            Thread.Sleep(10);
            var userLines = File.ReadAllLines(FilePath);

            File.Delete(FilePath);

            Thread.Sleep(10);
            var streamWriter = new StreamWriter(FilePath, true);
            var recorded = false;

            foreach (var userLine in userLines)
            {
                if (UserID(userLine) == userID)
                {
                    if (AwardID(userLine) == string.Empty)
                    {
                        streamWriter.Write(LineWithID(userLine, awardID));
                    }
                    else
                    {
                        streamWriter.Write(userLine);
                        streamWriter.WriteLine();

                        if (!recorded)
                        {
                            streamWriter.Write(LineWithID(userLine, awardID));
                        }

                        recorded = true;
                    }
                }
                else
                {
                    streamWriter.Write(userLine);
                }

                streamWriter.WriteLine();
            }

            streamWriter.Close();
        }

        private static string Name(string line) => line.Split(Separator)[User.GetFieldIndex("Name")];

        private static string UserID(string line) => line.Split(Separator)[User.GetFieldIndex("UserID")];

        private static string AwardID(string line) => line.Split(Separator)[User.GetFieldIndex("AwardID")];

        private static string LineWithID(string line, string id)
        {
            var itemArray = line.Split('|');
            var sb = new StringBuilder();

            int indexID = User.GetFieldIndex("AwardID");

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

        private void CheckFileExistance()
        {
            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException($"{nameof(FilePath)} is not exists!");
            }
        }

        public string GetID(string userName)
        {
            var userID = string.Empty;

            CheckFileExistance();

            Thread.Sleep(10);
            var userLines = File.ReadAllLines(FilePath);

            foreach (var userLine in userLines)
            {
                if (Name(userLine) == userName)
                {
                    return userLine.Split(Separator)[User.GetFieldIndex("UserID")];
                }
            }

            return userID;
        }
    }
}
