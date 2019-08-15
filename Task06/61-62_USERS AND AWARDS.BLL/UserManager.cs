using _61_62_USERS_AND_AWARDS.Entities;
using System;
using System.Globalization;
using System.Text;

namespace _61_62_USERS_AND_AWARDS.BLL
{
    public class UserManager
    {
        public readonly string DateFormat = "dd.MM.yyyy";

        public User Create() => new User(GetNameFromConsole(), GetDateFromConsole());

        private static string GetNameFromConsole()
        {
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

        public static void EmulateConsoleKeyBackSpace(StringBuilder userKeySB)
        {
            if (userKeySB.Length > 0)
            {
                userKeySB.Length--;
            }

            Console.Clear();
            StorageManager.PrintObjects();
            StorageManager.WriteMenu();

            Console.Write(userKeySB);
        }

        public DateTime GetDateFromConsole()
        {
            Console.WriteLine($"Enter date in format: {DateFormat}");

            bool isDate = false;

            DateTime userBirthDate = default;

            while (!isDate)
            {
                isDate = DateTime.TryParseExact(Console.ReadLine(), DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out userBirthDate);

                if (!isDate)
                {
                    Console.WriteLine($"Enter date in format: {DateFormat}");
                }
                else
                {
                    isDate = true;
                }
            }

            return userBirthDate;
        }
    }
}
