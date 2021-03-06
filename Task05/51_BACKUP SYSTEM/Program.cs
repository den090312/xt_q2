﻿using System;
using System.Globalization;
using System.Text;
using System.Threading;

namespace _51_BACKUP_SYSTEM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Storage.Create();
            Storage.WriteInfo();

            WriteMenu();

            bool inputComplete = false;

            while (!inputComplete)
            {
                inputComplete = GoToOperation(inputComplete, GetKeyFromConsole());
            }
        }

        private static bool GoToOperation(bool inputComplete, int userKey)
        {
            if (userKey != 0)
            {
                switch (userKey)
                {
                    case 1:
                        inputComplete = true;

                        new Thread(() => new Watcher().RunBackup()).Start();

                        break;
                    case 2:
                        inputComplete = true;

                        Console.WriteLine("--------START RESTORE--------");

                        var userDate = GetDateFromConsole(Log.DateFormat);
                        Storage.RestoreToDate(userDate);

                        Console.WriteLine("-------RESTORE IS DONE-------");
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }
            }

            return inputComplete;
        }

        private static DateTime GetDateFromConsole(string dateFormat)
        {
            Console.Clear();
            Console.WriteLine($"Enter date in format '{dateFormat}'");

            bool isDate = DateTime.TryParseExact(Console.ReadLine(), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime userDate);

            if (!isDate)
            {
                throw new ArgumentException($"Date must be in format '{dateFormat}'");
            }

            return userDate;
        }

        private static void WriteMenu()
        {
            Console.WriteLine("It's time to choose:");
            Console.WriteLine("\t1: watcher mode");
            Console.WriteLine("\t2: backup mode");
            Console.WriteLine("\t3: exit");
        }

        private static int GetKeyFromConsole()
        {
            bool inputComplete = false;

            StringBuilder userKeySB = new StringBuilder();

            while (!inputComplete)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    inputComplete = true;
                }
                else if (char.IsDigit(key.KeyChar) & (key.KeyChar == '1' || key.KeyChar == '2' || key.KeyChar == '3'))
                {
                    if (userKeySB.Length < 1)
                    {
                        userKeySB.Append(key.KeyChar);
                        Console.Write(key.KeyChar);
                    }
                }
            }

            int result;

            if (userKeySB.Length > 0)
            {
                result = int.Parse(userKeySB.ToString());
            }
            else
            {
                result = 0;
            }

            return result;
        }
    }
}
