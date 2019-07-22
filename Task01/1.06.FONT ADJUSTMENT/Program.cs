using System;
using System.Text;

namespace _1._06.FONT_ADJUSTMENT
{
    class Program
    {
        [Flags]
        enum Font
        {
            None = 0,
            Bold = 1,
            Italic = 2,
            Underline = 4
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Параметры надписи: None");
            WriteMenu();

            Font myFont = Font.None;
            bool inputComplete = false;
            while (!inputComplete)
            {
                int userKey = GetKeyFromConsole();
                if (userKey != 0)
                {
                    myFont = ChangeFont(myFont, userKey);
                    Console.WriteLine();
                    Console.WriteLine($"Параметры надписи: {myFont}");
                    WriteMenu();
                }
            }
        }

        static void WriteMenu()
        {
            Console.WriteLine("Введите:");
            Console.WriteLine("\t1: bold");
            Console.WriteLine("\t2: italic");
            Console.WriteLine("\t3: underline");
            Console.WriteLine("\t4: EXIT");
        }

        static Font ChangeFont(Font myFont, int consoleKey)
        {
            if (myFont.HasFlag((Font)consoleKey))
            {
                myFont ^= (Font)consoleKey;
            }
            else
            {
                myFont |= (Font)consoleKey;
            }

            return myFont;
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
                else if (char.IsDigit(key.KeyChar) & (key.KeyChar == '1' || key.KeyChar == '2' || key.KeyChar == '3' || key.KeyChar == '4'))
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
