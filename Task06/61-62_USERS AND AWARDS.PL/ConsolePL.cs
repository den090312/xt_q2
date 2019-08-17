using _61_62_USERS_AND_AWARDS.BLL;
using System;
using System.Globalization;
using System.Text;

namespace _61_62_USERS_AND_AWARDS.PL
{
    public class ConsolePL
    {
        public static readonly string dateFormat = "dd.MM.yyyy";

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

            var storageManager = new StorageManager();
            storageManager.CreateStorage();
            storageManager.PrintStorageInfo();

            WriteMenu();

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
                            AddUser(dateFormat);
                            break;
                        case 2:
                            inputComplete = true;
                            consoleSegment = ConsoleSegment.User;
                            DeleteUser();
                            break;
                        case 3:
                            inputComplete = true;
                            System.Console.WriteLine();
                            PrintUsers();
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
                            System.Console.WriteLine();
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

        public static void WriteMenu()
        {
            Console.WriteLine("Users operations:");
            Console.WriteLine("\t1: create");
            Console.WriteLine("\t2: delete");
            Console.WriteLine("\t3: print");
            Console.WriteLine();
            Console.WriteLine("Awards operations:");
            Console.WriteLine("\t4: create");
            Console.WriteLine("\t5: delete");
            Console.WriteLine("\t6: print");
            Console.WriteLine();
            Console.WriteLine("Connect operations:");
            Console.WriteLine("\t7: add award to user");
            Console.WriteLine();
            Console.WriteLine("\t8: exit");
        }

        private static void AddUser(string dateFormat)
        {
            var usermanager = new UserManager();
            var user = usermanager.CreateUser(GetUserString("name"), GetUserDate(dateFormat));

            usermanager.AddUser(user);
        }

        private static void DeleteUser() => new UserManager().RemoveUser(GetUserString("name"));

        private static void PrintUsers() => new UserManager().PrintUsers();

        private static void CreateAward() => new AwardManager().CreateAward(GetUserString("title"));

        private static void DeleteAward() => new AwardManager().RemoveAward(GetUserString("award"));

        private static void PrintAllAwards() => new AwardManager().PrintAwards();

        private static void AddAwardToUser()
        {
            consoleSegment = ConsoleSegment.User;
            var userName = GetUserString("name");

            consoleSegment = ConsoleSegment.Award;
            var award = GetUserString("title");

            new StorageManager().AddAwardToUser(userName, award);
        }

        private static int GetKeyFromConsole()
        {
            bool inputComplete = false;

            StringBuilder userKeySB = new StringBuilder();

            while (!inputComplete)
            {
                ConsoleKeyInfo key = System.Console.ReadKey(true);

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
                        System.Console.Write(key.KeyChar);
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
            System.Console.Clear();
            System.Console.WriteLine($"Enter {parameterName}:");

            bool inputComplete = false;

            StringBuilder userSB = new StringBuilder();

            while (!inputComplete)
            {
                ConsoleKeyInfo key = System.Console.ReadKey(true);

                if (key.Key == ConsoleKey.Backspace)
                {
                    EmulateConsoleKeyBackSpace(userSB);
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    if (userSB.Length > 0)
                    {
                        inputComplete = true;
                        System.Console.WriteLine();
                    }
                }
                else
                {
                    userSB.Append(key.KeyChar);
                    System.Console.Write(key.KeyChar);
                }
            }

            return userSB.ToString();
        }

        public static DateTime GetUserDate(string dateFormat)
        {
            System.Console.Clear();
            System.Console.WriteLine($"Enter date in format: {dateFormat}");

            bool isDate = false;

            DateTime userBirthDate = default;

            while (!isDate)
            {
                isDate = DateTime.TryParseExact(System.Console.ReadLine(), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out userBirthDate);

                if (!isDate)
                {
                    System.Console.WriteLine($"Enter date in format: {dateFormat}");
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

            System.Console.Clear();
            ConsoleRestore();
            System.Console.Write(userKeySB);
        }

        private static void ConsoleRestore()
        {
            switch (consoleSegment)
            {
                case ConsoleSegment.Main:
                    new StorageManager().PrintStorageInfo();
                    WriteMenu();
                    break;
                case ConsoleSegment.User:
                    System.Console.WriteLine("Enter name:");
                    break;
                case ConsoleSegment.Award:
                    System.Console.WriteLine("Enter title:");
                    break;
            }
        }
    }
}
