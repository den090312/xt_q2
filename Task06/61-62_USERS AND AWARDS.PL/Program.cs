using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _61_62_USERS_AND_AWARDS.PL
{
    public class Program
    {
        private static void Main(string[] args)
        {
            //Storage.Create();
            //Storage.WriteInfo();

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
                            break;
                        case 2:
                            inputComplete = true;
                            break;
                        case 3:
                            Environment.Exit(0);
                            break;
                    }
                }
            }
        }

        private static void WriteMenu()
        {
            Console.WriteLine("User operations:");
            Console.WriteLine("\t1: create");
            Console.WriteLine("\t2: delete");
            Console.WriteLine("\t3: exit");
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
                    EmulateConsoleKeyBackSpace(userKeySB);
                }
                else if (key.Key == ConsoleKey.Enter)
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

        static void EmulateConsoleKeyBackSpace(StringBuilder userKeySB)
        {
            if (userKeySB.Length > 0)
            {
                userKeySB.Length--;
            }

            Console.Clear();
            Storage.WriteInfo();

            WriteMenu();

            Console.Write(userKeySB);
        }
    }
}
