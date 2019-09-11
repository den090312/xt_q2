using Common;
using InterfacesBLL;
using System;
using System.Globalization;
using System.Text;

namespace PL
{
    public class ConsolePL
    {
        private IUserLogic userBLL;
        private IAwardLogic awardBLL;
        private IUserAwardLogic userAwardBLL;

        private ConsoleSegment consoleSegment = ConsoleSegment.None;

        public static readonly string dateFormat = "dd.MM.yyyy";

        private enum ConsoleSegment
        {
            None = 0,
            Main = 1,
            User = 2,
            Award = 3
        }

        public void Run()
        {
            SetBLL();
            RunInput();
        }

        private void SetBLL()
        {
            var dr = new DependencyResolver();

            userBLL = dr.UserBLL;
            awardBLL = dr.AwardBLL;
            userAwardBLL = dr.UserAwardBLL;
        }

        private void RunInput()
        {
            consoleSegment = ConsoleSegment.Main;

            bool inputComplete;

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
                            inputComplete = UserCreation();
                            break;
                        case 2:
                            inputComplete = UserRemoving();
                            break;
                        case 3:
                            inputComplete = UserPrinting();
                            break;
                        case 4:
                            inputComplete = AwardCreation();
                            break;
                        case 5:
                            inputComplete = AwardRemoving();
                            break;
                        case 6:
                            inputComplete = AwardPrinting();
                            break;
                        case 7:
                            inputComplete = JoinAwardToUser();
                            break;
                        case 8:
                            Console.WriteLine();
                            return true;
                    }
                }
            }

            return inputComplete;
        }

        private bool UserCreation()
        {
            consoleSegment = ConsoleSegment.User;
            CreateUser(dateFormat);
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private bool UserRemoving()
        {
            consoleSegment = ConsoleSegment.User;
            RemoveUsers();
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private bool UserPrinting()
        {
            consoleSegment = ConsoleSegment.User;
            Console.Clear();
            PrintUsers();
            Console.WriteLine();

            return InputComplete();
        }

        private bool AwardCreation()
        {
            consoleSegment = ConsoleSegment.Award;
            CreateAward();
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private bool AwardRemoving()
        {
            consoleSegment = ConsoleSegment.Award;
            RemoveAwards();
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private bool AwardPrinting()
        {
            consoleSegment = ConsoleSegment.Award;
            Console.Clear();
            PrintAwards();
            Console.WriteLine();

            return InputComplete();
        }

        private bool JoinAwardToUser()
        {
            Join();
            Console.WriteLine("---Done---");

            return InputComplete();
        }

        private void Join()
        {
            consoleSegment = ConsoleSegment.User;
            var userName = GetUserString("name");

            consoleSegment = ConsoleSegment.Award;
            var awardName = GetUserString("title");

            userAwardBLL.JoinAwardsToUsers(awardBLL.GetAwardIdArray(awardName), userBLL.GetUserIdArray(userName));
        }

        private void CreateUser(string dateFormat)
        {
            var user = userBLL.CreateUser(GetUserString("name"), GetUserDate(dateFormat));

            userBLL.AddUser(user);
        }

        private void RemoveUsers() => userBLL.RemoveUsers(GetUserString("name"));

        private void PrintUsers() => userBLL.PrintUsers();

        private void CreateAward()
        {
            var award = awardBLL.CreateAward(GetUserString("title"));

            awardBLL.AddAward(award);
        }

        private void RemoveAwards() => awardBLL.RemoveAwards(GetUserString("award"));

        private void PrintAwards() => awardBLL.PrintAwards();

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

        private int GetKeyFromConsole()
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

        private bool KeyTaken(bool inputComplete, StringBuilder userKeySB)
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