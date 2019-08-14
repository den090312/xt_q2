using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;

namespace _61_62_USERS_AND_AWARDS.DAL
{
    public static class Storage
    {
        private static readonly string messageNotFound = "not found";

        private static string Disk { get; }

        private static string Tom { get; }

        private static string Main { get; } 

        private static string Root { get; }

        private static string Users { get; } 

        private static string Awards { get; } 

        static Storage()
        {
            Disk   = "D";
            Tom    = $@"{Disk}:\";
            Main   = $"{Tom}Task06";
            Root   = $@"{Main}\Storage";
            Users  = $@"{Main}\Users.txt";
            Awards = $@"{Main}\Awards.txt";
        }

        public static void Create()
        {
            if (!File.Exists(Main))
            {
                Directory.CreateDirectory(Main);
            }

            if (!File.Exists(Root))
            {
                Directory.CreateDirectory(Root);
            }

            if (!File.Exists(Users))
            {
                Thread.Sleep(10);
                var streamWriter = new StreamWriter(Users, true);

                streamWriter.Write("");
                streamWriter.Close();
            }

            if (!File.Exists(Awards))
            {
                Thread.Sleep(10);
                var streamWriter = new StreamWriter(Awards, true);

                streamWriter.Write("");
                streamWriter.Close();
            }
        }

        public static void WriteInfo()
        {
            Console.WriteLine("---------Task folders--------");

            bool isFolder = true;

            WriteInfoPath(Main, "Main", isFolder);
            WriteInfoPath(Root, "Root", isFolder);

            isFolder = false;
            WriteInfoPath(Users, "Users", isFolder);
            WriteInfoPath(Awards, "Awards", isFolder);

            Console.WriteLine("-----------------------------");
        }

        private static void WriteInfoPath(string path, string name, bool isFolder)
        {
            if (isFolder & Directory.Exists(path))
            {
                Console.WriteLine($"{name} - {path}");
            }
            else if (!isFolder & File.Exists(path))
            {
                Console.WriteLine($"{name} - {path}");
            }
            else
            {
                Console.WriteLine($"{name} - {messageNotFound}");
            }
        }
    }
}
