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

        public static string Disk { get; } = "D";

        public static string Tom { get; } = $@"{Disk}:\";

        public static string Main { get; } = $"{Tom}Task06";

        public static string Root { get; } = $@"{Main}\Storage";

        public static string Users { get; } = $@"{Main}\Users.txt";

        public static string Awards { get; } = $@"{Main}\Awards.txt";

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

            WriteInfoPath(Main);
            WriteInfoPath(Root);
            WriteInfoPath(Users);
            WriteInfoPath(Awards);

            Console.WriteLine("-----------------------------");
        }

        private static void WriteInfoPath(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"{nameof(path)} - {Main}");
            }
            else
            {
                Console.WriteLine($"{nameof(path)} - {messageNotFound}");
            }
        }
    }
}
