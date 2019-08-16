using _61_62_USERS_AND_AWARDS.BLL;
using System;
using System.Globalization;
using System.Text;

namespace _61_62_USERS_AND_AWARDS.PL
{
    public class Program
    {
        public static readonly string DateFormat = "dd.MM.yyyy";

        private enum ConsoleSegment
        {
            None = 0,
            Main = 1,
            User = 2,
            Award = 3
        }

        private static ConsoleSegment consoleSegment = ConsoleSegment.None;

        private static void Main(string[] args)
        {
            consoleSegment = ConsoleSegment.Main;

            StorageManager.CreateStorage();
            StorageManager.PrintStoragePaths();
            StorageManager.WriteMenu();

            bool inputComplete = false;

            RunOperation(ref inputComplete);
        }

        private static void RunOperation(ref bool inputComplete)
        {
            while (!inputComplete)
            {
                int userKey = GetKeyFromConsole();

                if (userKey != 0)
                {
                    switch (userKey)
                    {
                        case 1:
                            inputComplete = true;
                            consoleSegment = ConsoleSegment.User;
                            CreateUser(DateFormat);
                            break;
                        case 2:
                            inputComplete = true;
                            consoleSegment = ConsoleSegment.User;
                            DeleteUser();
                            break;
                        case 3:
                            inputComplete = true;
                            Console.WriteLine();
                            PrintAllUsers();
                            break;
                        case 4:
                            inputComplete = true;
                            consoleSegment = ConsoleSegment.Award;
                            CreateAward();
                            break;
                        case 5:
                            inputComplete = true;
                            consoleSegment = ConsoleSegment.Award;
                            DeleteAward();
                            break;
                        case 6:
                            inputComplete = true;
                            Console.WriteLine();
                            PrintAllAwards();
                            break;
                        case 7:
                            inputComplete = true;
                            AddAwardToUser();
                            break;
                        case 8:
                            return;
                    }
                }
            }
        }

        private static void CreateUser(string dateFormat) => UserManager.CreateUser(GetUserString("name"), GetDate(dateFormat));

        private static void DeleteUser() => UserManager.DeleteUser(GetUserString("name"));

        private static void PrintAllUsers() => UserManager.PrintAllUsers();

        private static void CreateAward() => AwardManager.CreateAward(GetUserString("title"));

        private static void DeleteAward() => AwardManager.DeleteAward(GetUserString("award"));

        private static void PrintAllAwards() => AwardManager.PrintAllAwards();

        private static void AddAwardToUser()
        {
            consoleSegment = ConsoleSegment.User;
            var name = GetUserString("name");

            consoleSegment = ConsoleSegment.Award;
            var award = GetUserString("title");

            StorageManager.AddAwardToUser(name, award);
        }

        private static int GetKeyFromConsole()
        {
            bool inputComplete = false;

            StringBuilder userKeySB = new StringBuilder();

            while (!inputComplete)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                char[] keyArray = { '1', '2', '3', '4', '5', '6', '7', '8'};

                if (key.Key == ConsoleKey.Backspace)
                {
                    EmulateConsoleKeyBackSpace(userKeySB);
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    inputComplete = true;
                }
                else if ((Array.Exists(keyArray, x => x == key.KeyChar)))
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

        private static string GetUserString(string parameterName)
        {
            Console.Clear();
            Console.WriteLine($"Enter {parameterName}:");

            bool inputComplete = false;

            StringBuilder userSB = new StringBuilder();

            while (!inputComplete)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Backspace)
                {
                    EmulateConsoleKeyBackSpace(userSB);
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    inputComplete = true;
                    Console.WriteLine();
                }
                else
                {
                    userSB.Append(key.KeyChar);
                    Console.Write(key.KeyChar);
                }
            }

            return userSB.ToString();
        }

        public static DateTime GetDate(string dateFormat)
        {
            Console.Clear();
            Console.WriteLine($"Enter date in format: {dateFormat}");

            bool isDate = false;

            DateTime userBirthDate = default;

            while (!isDate)
            {
                isDate = DateTime.TryParseExact(Console.ReadLine(), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out userBirthDate);

                if (!isDate)
                {
                    Console.WriteLine($"Enter date in format: {dateFormat}");
                }
                else
                {
                    isDate = true;
                }
            }

            return userBirthDate;
        }

        public static void EmulateConsoleKeyBackSpace(StringBuilder userKeySB)
        {
            if (userKeySB.Length > 0)
            {
                userKeySB.Length--;
            }

            Console.Clear();
            ConsoleRestore();
            Console.Write(userKeySB);
        }

        private static void ConsoleRestore()
        {
            switch (consoleSegment)
            {
                case ConsoleSegment.Main:
                    StorageManager.PrintStoragePaths();
                    StorageManager.WriteMenu();
                    break;
                case ConsoleSegment.User:
                    Console.WriteLine("Enter name:");
                    break;
                case ConsoleSegment.Award:
                    Console.WriteLine("Enter title:");
                    break;
            }
        }
    }
}
