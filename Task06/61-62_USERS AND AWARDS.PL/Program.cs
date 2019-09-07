using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Task06.BLL;
using Task06.Entities;
using Task06.Interfaces;

namespace Task06.PL
{
    public class Program
    {
        private readonly static IUserable userManager;
        private readonly static IAwardable awardManager;

        private static ConsoleSegment consoleSegment = ConsoleSegment.None;

        private static readonly string dateFormat = "dd.MM.yyyy";

        static Program()
        {
            userManager = UserManager.UserImplement;
            awardManager = AwardManager.AwardImplement;
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
                            inputComplete = UserAdded();
                            break;
                        case 2:
                            inputComplete = UsersRemoved();
                            break;
                        case 3:
                            inputComplete = UsersPrinted();
                            break;
                        case 4:
                            inputComplete = AwardAdded();
                            break;
                        case 5:
                            inputComplete = AwardsRemoved();
                            break;
                        case 6:
                            inputComplete = AwardsPrinted();
                            break;
                        case 7:
                            inputComplete = AwardToUserJoined();
                            break;
                        case 8:
                            Console.WriteLine();
                            return true;
                    }
                }
            }

            return inputComplete;
        }

        private static bool UserAdded()
        {
            consoleSegment = ConsoleSegment.User;

            var user = userManager.CreateUser(GetUserString("name"), GetUserDate(dateFormat));
            userManager.AddUser(user);
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private static bool UsersRemoved()
        {
            consoleSegment = ConsoleSegment.User;

            userManager.RemoveUsers(GetUserString("name"));
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private static bool AwardAdded()
        {
            consoleSegment = ConsoleSegment.Award;

            var award = awardManager.CreateAward(GetUserString("title"));
            awardManager.AddAward(award);
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private static bool AwardToUserJoined()
        {
            consoleSegment = ConsoleSegment.User;
            var userName = GetUserString("name");

            consoleSegment = ConsoleSegment.Award;
            var awardName = GetUserString("title");

            userManager.Join(userName, awardName);
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private static bool AwardsPrinted()
        {
            Console.WriteLine();
            awardManager.PrintAwards();
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private static bool AwardsRemoved()
        {
            consoleSegment = ConsoleSegment.Award;

            awardManager.RemoveAwards(GetUserString("award"));
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private static bool UsersPrinted()
        {
            Console.WriteLine();
            userManager.PrintUsers(awardManager.GetAwardList());
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

        private static int GetKeyFromConsole()
        {
            bool inputComplete = false;

            StringBuilder userKeySB = new StringBuilder();

            while (!inputComplete)
            {
                inputComplete = KeyTaken(inputComplete, userKeySB);
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

        private static bool KeyTaken(bool inputComplete, StringBuilder userKeySB)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            char[] keyArray = { '1', '2', '3', '4', '5', '6', '7', '8' };

            if (key.Key == ConsoleKey.Backspace)
            {
                EmulateBackspace(userKeySB);
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                if (userKeySB.Length > 0)
                {
                    inputComplete = true;
                }
            }
            else if ((Array.Exists(keyArray, x => x == key.KeyChar)))
            {
                if (userKeySB.Length < 1)
                {
                    userKeySB.Append(key.KeyChar);
                    Console.Write(key.KeyChar);
                }
            }

            return inputComplete;
        }

        private static string GetUserString(string parameterName)
        {
            Console.Clear();
            Console.WriteLine($"Enter {parameterName}:");

            bool inputComplete = false;

            StringBuilder userSB = new StringBuilder();

            while (!inputComplete)
            {
                inputComplete = InputComplete(inputComplete, userSB);
            }

            return userSB.ToString();
        }

        private static bool InputComplete(bool inputComplete, StringBuilder userSB)
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

            return inputComplete;
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