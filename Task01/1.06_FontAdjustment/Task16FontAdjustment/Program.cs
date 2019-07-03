using System;
using System.Text;

namespace Task16FontAdjustment
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
                    Console.WriteLine("");
                    Console.WriteLine("Параметры надписи: "+ myFont);
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
            switch (consoleKey)
            {
                case 1:
                    myFont = ChangeFontFromFlag(myFont, Font.Bold);
                    break;
                case 2:
                    myFont = ChangeFontFromFlag(myFont, Font.Italic);
                    break;
                case 3:
                    myFont = ChangeFontFromFlag(myFont, Font.Underline);
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
            }

            return myFont;
        }

        static Font ChangeFontFromFlag(Font font, Font fontFlag)
        {
            if (font.HasFlag(fontFlag))
            {
                font ^= fontFlag;
            }
            else
            {
                font |= fontFlag;
            }

            return font;
        }

        static int GetKeyFromConsole()
        {
            bool inputComplete = false;
            StringBuilder mySb = new StringBuilder();
            while (!inputComplete)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    inputComplete = true;
                }
                else if (char.IsDigit(key.KeyChar) & (key.KeyChar == '1' || key.KeyChar == '2' || key.KeyChar == '3' || key.KeyChar == '4'))
                {
                    if (mySb.Length < 1)
                    {
                        mySb.Append(key.KeyChar);
                        Console.Write(key.KeyChar.ToString());
                    }
                }
            }

            int result;
            if (mySb.Length > 0)
            {
                result = int.Parse(mySb.ToString());
            }
            else
            {
                result = 0;
            }

            return result;
        }
    }
}
