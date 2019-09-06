using System;
using System.Globalization;
using System.Text;
using Task06.Common;
using Task06.Interfaces;

namespace Task06.PL
{
    public class Program
    {
        private readonly static IUserable userManager;
        private readonly static IAwardable awardManager;

        private static ConsoleSegment consoleSegment = ConsoleSegment.None;

        public static readonly string dateFormat = "dd.MM.yyyy";

        static Program()
        {
            userManager = Dependencies.UserManager;
            awardManager = Dependencies.AwardManager;
        }

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

            CreateStorage();
            PrintStorageInfo();
            Console.WriteLine();

            var inputComplete = false;

            do
            {
                inputComplete = InputComplete();
            }
            while (!inputComplete);
        }

        private static void CreateStorage()
        {
            userManager.CreateStorage();
            awardManager.CreateStorage();
        }

        private static void PrintStorageInfo()
        {
            userManager.PrintStorageInfo();
            awardManager.PrintStorageInfo();
        }

        private static bool InputComplete()
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
                            inputComplete = StartUserCreation();
                            break;
                        case 2:
                            inputComplete = StartUserRemoving();
                            break;
                        case 3:
                            inputComplete = StartUserPrinting();
                            break;
                        case 4:
                            inputComplete = StartAwardCreation();
                            break;
                        case 5:
                            inputComplete = StartAwardRemoving();
                            break;
                        case 6:
                            inputComplete = StartAwardPrinting();
                            break;
                        case 7:
                            inputComplete = StartJoin();
                            break;
                        case 8:
                            Console.WriteLine();
                            return true;
                    }
                }
            }

            return inputComplete;
        }

        private static bool StartJoin()
        {
            JoinAwardToUser();
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private static bool StartAwardPrinting()
        {
            Console.WriteLine();
            PrintAwards();
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private static bool StartAwardRemoving()
        {
            consoleSegment = ConsoleSegment.Award;
            RemoveAward();
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private static bool StartAwardCreation()
        {
            consoleSegment = ConsoleSegment.Award;
            CreateAward();
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private static bool StartUserPrinting()
        {
            Console.WriteLine();
            PrintUsers();
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private static bool StartUserRemoving()
        {
            consoleSegment = ConsoleSegment.User;
            RemoveUser();
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private static bool StartUserCreation()
        {
            consoleSegment = ConsoleSegment.User;
            CreateUser(dateFormat);
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private static void WriteMenu()
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
            var user = userManager.CreateUser(GetUserString("name"), GetUserDate(dateFormat));

            userManager.AddUser(user);
        }

        private static void RemoveUser() => userManager.RemoveUsers(GetUserString("name"));

        private static void PrintUsers() => userManager.PrintUsers(awardManager.GetAwardList());

        private static void CreateAward()
        {
            var award = awardManager.CreateAward(GetUserString("title"));

            awardManager.AddAward(award);
        }

        private static void RemoveAward() => awardManager.RemoveAwards(GetUserString("award"));

        private static void PrintAwards() => awardManager.PrintAwards();

        private static void JoinAwardToUser()
        {
            consoleSegment = ConsoleSegment.User;
            var userName = GetUserString("name");

            consoleSegment = ConsoleSegment.Award;
            var awardName = GetUserString("title");

            userManager.JoinAwardToUser(awardName, userName);
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
                    EmulateBackspace(userKeySB);
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
                    EmulateBackspace(userSB);
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

        private static DateTime GetUserDate(string dateFormat)
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

        private static void EmulateBackspace(StringBuilder userKeySB)
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
                    PrintStorageInfo();
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