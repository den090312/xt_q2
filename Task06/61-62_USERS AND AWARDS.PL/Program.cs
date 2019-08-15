using _61_62_USERS_AND_AWARDS.BLL;
using System;
using System.Text;

namespace _61_62_USERS_AND_AWARDS.PL
{
    public class Program
    {
        private static void Main(string[] args)
        {
            StorageManager.Create();
            StorageManager.PrintObjects();
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
                            CreateUser();
                            break;
                        case 2:
                            inputComplete = true;
                            break;
                        case 3:
                            return;
                    }
                }
            }
        }

        private static void CreateUser()
        {
            var userManager = new UserManager();
        }

        private static int GetKeyFromConsole()
        {
            bool inputComplete = false;

            StringBuilder userKeySB = new StringBuilder();

            while (!inputComplete)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Backspace)
                {
                    UserManager.EmulateConsoleKeyBackSpace(userKeySB);
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    inputComplete = true;
                }
                else if ((key.KeyChar == '1' || key.KeyChar == '2' || key.KeyChar == '3'))
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
