using Task06.BLL;
using System;
using System.Globalization;
using System.Text;

namespace Task06.PL
{
    public class Program
    {
        private static ConsoleSegment consoleSegment = ConsoleSegment.None;

        public static readonly string dateFormat = "dd.MM.yyyy";

        private enum ConsoleSegment
        {
            None = 0,
            Main = 1,
            User = 2,
            Award = 3
        }

        private static void Main(string[] args)
        {
            consoleSegment = ConsoleSegment.Main;

            new UserManager().PrintStorageInfo();
            new AwardManager().PrintStorageInfo();
            Console.WriteLine();

            var inputComplete = false;

            do
            {
                inputComplete = RunOperation();
            }
            while (!inputComplete);
        }

        private static bool RunOperation()
        {
            WriteMenu();

            var userKey = GetKeyFromConsole();

            var inputComplete = false;

            while (!inputComplete)
            {
                if (userKey != 0)
                {
                    switch (userKey)
                    {
                        case 1:
                            inputComplete = CreateUserStart();
                            break;
                        case 2:
                            inputComplete = RemoveUserStart();
                            break;
                        case 3:
                            inputComplete = PrintUsersStart();
                            break;
                        case 4:
                            inputComplete = CreateAwardStart();
                            break;
                        case 5:
                            inputComplete = RemoveAwardStart();
                            break;
                        case 6:
                            inputComplete = PrintAwardsStart();
                            break;
                        case 7:
                            inputComplete = Join();
                            break;
                        case 8:
                            return true;
                    }
                }
            }

            return inputComplete;
        }

        private static bool Join()
        {
            JoinAwardToUser();
            Console.WriteLine("---Done---");

            return RunOperation();
        }

        private static bool PrintAwardsStart()
        {
            Console.WriteLine();
            PrintAwards();
            Console.WriteLine("---Done---");

            return RunOperation();
        }

        private static bool RemoveAwardStart()
        {
            consoleSegment = ConsoleSegment.Award;
            RemoveAward();
            Console.WriteLine("---Done---");

            return RunOperation();
        }

        private static bool CreateAwardStart()
        {
            consoleSegment = ConsoleSegment.Award;
            CreateAward();
            Console.WriteLine("---Done---");

            return RunOperation();
        }

        private static bool PrintUsersStart()
        {
            Console.WriteLine();
            PrintUsers();
            Console.WriteLine("---Done---");

            return RunOperation();
        }

        private static bool RemoveUserStart()
        {
            consoleSegment = ConsoleSegment.User;
            RemoveUser();
            Console.WriteLine("---Done---");

            return RunOperation();
        }

        private static bool CreateUserStart()
        {
            consoleSegment = ConsoleSegment.User;
            CreateUser(dateFormat);
            Console.WriteLine("---Done---");

            return RunOperation();
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
            Console.WriteLine("Join operations:");
            Console.WriteLine("\t7: join award to user");
            Console.WriteLine();
            Console.WriteLine("\t8: exit");
        }

        private static void CreateUser(string dateFormat)
        {
            var userManager = new UserManager();
            var user = userManager.CreateUser(GetUserString("name"), GetUserDate(dateFormat));

            userManager.AddUser(user);
        }

        private static void RemoveUser() => new UserManager().RemoveUsers(GetUserString("name"));

        private static void PrintUsers() => new UserManager().PrintUsers(new AwardManager().GetAwardList());

        private static void CreateAward()
        {
            var awardManager = new AwardManager();
            var award = awardManager.CreateAward(GetUserString("title"));

            awardManager.AddAward(award);
        }

        private static void RemoveAward() => new AwardManager().RemoveAwards(GetUserString("award"));

        private static void PrintAwards() => new AwardManager().PrintAwards();

        private static void JoinAwardToUser()
        {
            consoleSegment = ConsoleSegment.User;
            var userName = GetUserString("name");

            consoleSegment = ConsoleSegment.Award;
            var awardName = GetUserString("title");

            new UserManager().JoinAwardToUser(awardName, userName);
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
