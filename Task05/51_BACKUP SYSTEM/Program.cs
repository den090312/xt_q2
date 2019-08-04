using System;
using System.Globalization;
using System.Text;

namespace _51_BACKUP_SYSTEM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Storage.WriteInfo();

            WriteMenu();

            bool inputComplete = false;

            while (!inputComplete)
            {
                int userKey = GetKeyFromConsole();

                if (userKey != 0)
                {

                    if (userKey == 1)
                    {
                        inputComplete = true;

                        new Watcher().Run();
                    }

                    if (userKey == 2)
                    {
                        inputComplete = true;

                        var userDate = GetDateFromConsole(LogData.DateFormat);

                        Storage.RestoreToDate(userDate);
                    }

                    if (userKey == 3)
                    {
                        Environment.Exit(0);
                    }
                }
            }
        }

        public static DateTime GetDateFromConsole(string dateFormat)
        {
            Console.Clear();
            Console.WriteLine($"Enter date in format: {dateFormat}");

            bool isDate = DateTime.TryParseExact(Console.ReadLine(), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime userDate);

            if (!isDate)
            {
                throw new ArgumentException($"Date must be in format: {dateFormat}:");
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
