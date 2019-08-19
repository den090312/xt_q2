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

            new UserManager().PrintStorageInfo();
            new AwardManager().PrintStorageInfo();

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
                            CreateUser(dateFormat);
                            break;
                        case 2:
                            inputComplete = true;
                            consoleSegment = ConsoleSegment.User;
                            RemoveUser();
                            break;
                        case 3:
                            inputComplete = true;
                            Console.WriteLine();
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
                            RemoveAward();
                            break;
                        case 6:
                            inputComplete = true;
                            Console.WriteLine();
                            PrintAwards();
                            break;
                        case 7:
                            inputComplete = true;
                            PinAwardToUser();
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
            Console.WriteLine("\t7: pin award to user");
            Console.WriteLine();
            Console.WriteLine("\t8: exit");
        }

        private static void CreateUser(string dateFormat)
        {
            var userManager = new UserManager();
            var user = userManager.CreateUser(GetUserString("name"), GetUserDate(dateFormat));

            userManager.AddUser(user);
        }

        private static void RemoveUser() => new UserManager().RemoveUser(GetUserString("name"));

        private static void PrintUsers() => new UserManager().PrintUsers(new AwardManager().GetAwards());

        private static void CreateAward()
        {
            var awardManager = new AwardManager();
            var award = awardManager.CreateAward(GetUserString("title"));

            awardManager.AddAward(award);
        }

        private static void RemoveAward() => new AwardManager().RemoveAward(GetUserString("award"));

        private static void PrintAwards() => new AwardManager().PrintAwards();

        private static void PinAwardToUser()
        {
            consoleSegment = ConsoleSegment.User;
            var userName = GetUserString("name");

            consoleSegment = ConsoleSegment.Award;
            var awardName = GetUserString("title");

            new UserManager().PinAwardToUser(awardName, userName);
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
                    if (userSB.Length > 0)
                    {
                        inputComplete = true;
                        Console.WriteLine();
                    }
                }
                else
                {
                    userSB.Append(key.KeyChar);
                    Console.Write(key.KeyChar);
                }
            }

            return userSB.ToString();
        }

        public static DateTime GetUserDate(string dateFormat)
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
                    new UserManager().PrintStorageInfo();
                    new AwardManager().PrintStorageInfo();
                    WriteMenu();
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
