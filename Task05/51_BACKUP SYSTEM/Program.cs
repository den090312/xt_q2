using System;
using System.IO;
using System.Text;

namespace _51_BACKUP_SYSTEM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WriteMenu();

            bool inputComplete = false;

            while (!inputComplete)
            {
                int userKey = GetKeyFromConsole();

                if (userKey != 0)
                {

                    if (userKey == 1)
                    {
                        inputComplete = true;

                        new Watcher().Run();
                    }

                    if (userKey == 2)
                    {

                    }

                    if (userKey == 3)
                    {
                        Environment.Exit(0);
                    }
                }
            }
        }

        static void WriteMenu()
        {
            Console.WriteLine("Сделай свой выбор:");
            Console.WriteLine("\t1: режим наблюдения");
            Console.WriteLine("\t2: режим отката изменений");
            Console.WriteLine("\t3: выход");
        }

        static int GetKeyFromConsole()
        {
            bool inputComplete = false;

            StringBuilder userKeySB = new StringBuilder();

            while (!inputComplete)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
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
    }
}
