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
            Console.WriteLine($"Main - {Main}");
            Console.WriteLine($"Root - {Root}");
            Console.WriteLine($"Users - {Users}");
            Console.WriteLine($"Awards - {Awards}");
            Console.WriteLine("-----------------------------");
        }
    }
}
