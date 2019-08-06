using System;
using System.Globalization;
using System.Text;

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
                int userKey = GetKeyFromConsole();

                if (userKey != 0)
                {
                    switch (userKey)
                    {
                        case 1:
                            inputComplete = true;

                            Console.Clear();
                            Console.WriteLine("Watcher mode is on. Press '3' to exit");
                            while (Console.Read() != '3') ;

                            Backup();
                            break;
                        case 2:
                            inputComplete = true;
                            Restore();
                            break;
                        case 3:
                            Environment.Exit(0);
                            break;
                    }
                }
            }
        }

        private static void Backup()
        {
            var guid = Guid.NewGuid().ToString();

            Console.WriteLine("--------START BACKUP--------");
            Storage.CreateBackup(guid);
            Console.WriteLine("-------BACKUP IS DONE-------");
        }

        private static void Restore()
        {
            var userDate = GetDateFromConsole(Log.DateFormat);

            Console.WriteLine("--------START RESTORE--------");
            Storage.RestoreToDate(userDate);
            Console.WriteLine("-------RESTORE IS DONE-------");
        }

        public static DateTime GetDateFromConsole(string dateFormat)
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
