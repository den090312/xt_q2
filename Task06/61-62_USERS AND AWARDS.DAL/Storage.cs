using _61_62_USERS_AND_AWARDS.Interfaces;
using System;
using System.IO;
using System.Threading;

namespace _61_62_USERS_AND_AWARDS.DAL
{
    public class Storage : IStorable
    {
        private static readonly string messageNotFound = "not found";

        private static string Disk { get; }

        private static string Tom { get; }

        private static string Main { get; } 

        private static string Root { get; }

        public static string Users { get; } 

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

        public void Create()
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

        public void PrintAllPaths()
        {
            Console.WriteLine("---------Task folders--------");

            bool isFolder = true;

            PrintSinglePath(Main, "Main", isFolder);
            PrintSinglePath(Root, "Root", isFolder);

            isFolder = false;
            PrintSinglePath(Users, "Users", isFolder);
            PrintSinglePath(Awards, "Awards", isFolder);

            Console.WriteLine("-----------------------------");
        }

        private static void PrintSinglePath(string path, string name, bool isFolder)
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
