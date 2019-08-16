using _61_62_USERS_AND_AWARDS.BLL;
using System;
using System.Globalization;
using System.Text;

namespace _61_62_USERS_AND_AWARDS.PL
{
    public class Program
    {
        public static readonly string DateFormat = "dd.MM.yyyy";

        private static void Main(string[] args)
        {
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
                            CreateUser(DateFormat);
                            break;
                        case 2:
                            inputComplete = true;
                            DeleteUser();
                            break;
                        case 3:
                            inputComplete = true;
                            PrintAllUsers();
                            break;
                        case 4:
                            inputComplete = true;
                            CreateAward();
                            break;
                        case 5:
                            inputComplete = true;
                            DeleteAward();
                            break;
                        case 6:
                            inputComplete = true;
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

        private static void AddAwardToUser() => StorageManager.AddAwardToUser(GetUserString("user name"), GetUserString("award"));

        private static int GetKeyFromConsole()
        {
            bool inputComplete = false;

            StringBuilder userKeySB = new StringBuilder();

            while (!inputComplete)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Backspace)
                {
                    EmulateConsoleKeyBackSpace(userKeySB);
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    inputComplete = true;
                }
                else if ((key.KeyChar == '1' || key.KeyChar == '2' || key.KeyChar == '3' || key.KeyChar == '4'))
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
            Console.WriteLine("Enter name:");
            Console.Write(userKeySB);
        }
    }
}
