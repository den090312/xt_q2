using Common;
using Pl;
using System;
using System.Globalization;
using System.Text;

namespace PL
{
    internal class InputPl
    {
        private enum ConsoleSegment
        {
            None = 0,
            Main = 1,
            User = 2,
            Award = 3
        }

        private ConsoleSegment consoleSegment = ConsoleSegment.None;

        private static readonly string dateFormat = "dd.MM.yyyy";

        internal void Run()
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

            var userKey = GetKeyFromConsole(8);

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
                            inputComplete = UsersAwardsPrint();
                            break;
                        case 4:
                            inputComplete = AwardCreation();
                            break;
                        case 5:
                            inputComplete = AwardRemoving();
                            break;
                        case 6:
                            inputComplete = AwardsPrint();
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

        private bool UserCreation()
        {
            var outputPl = new OutputPl();

            var user = outputPl?.CreateUser(dateFormat);

            if (outputPl.UserAdded(user))
            {
                Console.WriteLine($"---'{user.Name}' added---");
            }
            else
            {
                Console.WriteLine($"---'{user.Name}' NOT added---");
            }

            return InputComplete();
        }

        private bool UserRemoving()
        {
            Console.Clear();

            new OutputPl()?.RemoveUser();

            return InputComplete();
        }

        private bool UsersAwardsPrint()
        {
            consoleSegment = ConsoleSegment.User;
            Console.Clear();

            var users = new DependencyResolver()?.UserBll?.GetAll();
            new OutputPl()?.PrintUserAwards(users);

            Console.WriteLine();

            return InputComplete();
        }

        private bool AwardRemoving()
        {
            consoleSegment = ConsoleSegment.Award;
            new OutputPl()?.RemoveAward();

            return InputComplete();
        }

        private bool AwardsPrint()
        {
            consoleSegment = ConsoleSegment.Award;
            Console.Clear();

            new OutputPl()?.PrintAwards();
            Console.WriteLine();

            return InputComplete();
        }

        private bool AwardCreation()
        {
            consoleSegment = ConsoleSegment.User;

            var outputPl = new OutputPl();

            var award = outputPl?.CreateAward();

            if (outputPl.AwardAdded(award))
            {
                Console.WriteLine($"---'{award.Title}' added---");
            }
            else
            {
                Console.WriteLine($"---'{award.Title}' NOT added---");
            }

            return InputComplete();
        }

        private bool JoinAwardToUser()
        {
            var outputPl = new OutputPl();

            Console.WriteLine("Choose user by number:");
            Console.WriteLine("----------------------");

            var userGuid = outputPl.GetChosenUserGuid();
            Console.WriteLine();

            Console.WriteLine("Choose award by number:");
            Console.WriteLine("----------------------");

            var awardGuid = outputPl.GetChosenAwardGuid();
            Console.WriteLine();

            var userName = outputPl?.GetUserNameByGuid(userGuid);
            var awardName = outputPl?.GetAwardNameByGuid(awardGuid);

            if (new DependencyResolver().UserAwardBll.JoinedAwardToUser(userGuid, awardGuid))
            {
                Console.WriteLine($"---'{awardName}' joined to '{userName}'---");
            }
            else
            {
                Console.WriteLine($"---'{awardName}' NOT joined to '{userName}'---");
            }

            return InputComplete();
        }

        public string GetUserString(string parameterName)
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

        public DateTime GetUserDate(string dateFormat)
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

        public int[] GetKeyArray(int lastKey)
        {
            var keyArray = new int[lastKey + 1];

            for (var i = 1; i <= lastKey; i++)
            {
                keyArray[i] = i;
            }

            return keyArray;
        }

        public int GetKeyFromConsole(int lastKey)
        {
            var keyArray = GetKeyArray(lastKey);

            bool inputComplete = false;

            StringBuilder userKeySB = new StringBuilder();

            while (!inputComplete)
            {
                inputComplete = KeyTaken(inputComplete, userKeySB, keyArray);
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

        public bool KeyTaken(bool inputComplete, StringBuilder userKeySB, int[] keyArray)
        {
            var key = Console.ReadKey(true);

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
            else if ((Array.Exists(keyArray, x => x.ToString() == key.KeyChar.ToString())))
            {
                if (userKeySB.Length < 1)
                {
                    userKeySB.Append(key.KeyChar);
                    Console.Write(key.KeyChar);
                }
            }

            return inputComplete;
        }

        public void EmulateBackspace(StringBuilder userKeySB)
        {
            if (userKeySB.Length > 0)
            {
                userKeySB.Length--;
            }

            Console.Clear();
            ConsoleRestore();
            Console.Write(userKeySB);
        }

        public void ConsoleRestore()
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