using System;
using System.Text;

namespace PL
{
    public class ConsolePL
    {
        private static ConsoleSegment consoleSegment = ConsoleSegment.None;

        private enum ConsoleSegment
        {
            None = 0,
            Main = 1,
            User = 2,
            Award = 3
        }

        public void Run()
        {
            consoleSegment = ConsoleSegment.Main;

            Console.WriteLine();

            var inputComplete = false;

            do
            {
                inputComplete = InputComplete();
            }
            while (!inputComplete);
        }

        private bool InputComplete()
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

        private bool StartJoin()
        {
            JoinAwardToUser();
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private bool StartAwardPrinting()
        {
            Console.WriteLine();
            PrintAwards();
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private bool StartAwardRemoving()
        {
            consoleSegment = ConsoleSegment.Award;
            RemoveAward();
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private bool StartAwardCreation()
        {
            consoleSegment = ConsoleSegment.Award;
            CreateAward();
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private bool StartUserPrinting()
        {
            Console.WriteLine();
            PrintUsers();
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private bool StartUserRemoving()
        {
            consoleSegment = ConsoleSegment.User;
            RemoveUser();
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private bool StartUserCreation()
        {
            consoleSegment = ConsoleSegment.User;
            CreateUser(dateFormat);
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private void JoinAwardToUser()
        {
            consoleSegment = ConsoleSegment.User;
            var userName = GetUserString("name");

            consoleSegment = ConsoleSegment.Award;
            var awardName = GetUserString("title");

            userManager.JoinAwardToUser(awardName, userName);
        }

        private void WriteMenu()
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

        private string GetUserString(string parameterName)
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

        private int GetKeyFromConsole()
        {
            bool inputComplete = false;

            StringBuilder userKeySB = new StringBuilder();

            while (!inputComplete)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                char[] keyArray = { '1', '2', '3', '4', '5', '6', '7', '8' };

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

        private void EmulateBackspace(StringBuilder userKeySB)
        {
            if (userKeySB.Length > 0)
            {
                userKeySB.Length--;
            }

            Console.Clear();
            ConsoleRestore();
            Console.Write(userKeySB);
        }

        private void ConsoleRestore()
        {
            switch (consoleSegment)
            {
                case ConsoleSegment.Main:
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
