using System;
using System.Text;

namespace Task16FontAdjustment
{
    class Program
    {
        [Flags]
        enum FontAdjustment
        {
            None = 0,
            Bold = 1,
            Italic = 2,
            Underline = 4
        }

        static void Main(string[] args)
        {
            FontAdjustment fontAdjustment = FontAdjustment.None;
            Console.WriteLine("Параметры надписи: None");
            WriteMenu();

            bool inputComplete = false;
            while (!inputComplete)
            {
                int resultKey = GetKeyFromConsole();
                if (resultKey != 0)
                {
                    fontAdjustment = ChangeFontAdjustmentFromKey(fontAdjustment, resultKey);

                    Console.WriteLine("");
                    Console.WriteLine("Параметры надписи: " + GetCurrentAdjusment(fontAdjustment));
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

        static FontAdjustment GetCurrentAdjusment(FontAdjustment fontAdjustment)
        {
            if (fontAdjustment.HasFlag(FontAdjustment.Bold))
            {
                return fontAdjustment |= FontAdjustment.Bold;    
            }                

            if (fontAdjustment.HasFlag(FontAdjustment.Italic))
            {
                return fontAdjustment |= FontAdjustment.Italic;    
            }                

            if (fontAdjustment.HasFlag(FontAdjustment.Underline))
            {
                return fontAdjustment |= FontAdjustment.Underline;    
            }                

            return fontAdjustment;
        }

        static FontAdjustment ChangeFontAdjustmentFromKey(FontAdjustment fontAdjustment, int consoleKey)
        {
            switch (consoleKey)
            {              
                case 1:
                    fontAdjustment = SetFontAdjusment(fontAdjustment, FontAdjustment.Bold);
                    break;

                case 2:
                    fontAdjustment = SetFontAdjusment(fontAdjustment, FontAdjustment.Italic);
                    break;

                case 3:
                    fontAdjustment = SetFontAdjusment(fontAdjustment, FontAdjustment.Underline);
                    break;
                
                case 4:
                    Environment.Exit(0);
                    break;
            }

            return fontAdjustment;
        }

        static FontAdjustment SetFontAdjusment(FontAdjustment fontAdjustment, FontAdjustment fontAdjustmentFlag)
        {
            if (fontAdjustment.HasFlag(fontAdjustmentFlag))
            {
                fontAdjustment ^= fontAdjustmentFlag;   
            }                   
            else
            {
                fontAdjustment |= fontAdjustmentFlag;
            }                

            return fontAdjustment;
        }

        static int GetKeyFromConsole()
        {
            bool inputComplete = false;
            StringBuilder sb = new StringBuilder();
            while (!inputComplete)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    inputComplete = true;
                }
                else if (char.IsDigit(key.KeyChar) & (key.KeyChar == '1' || key.KeyChar == '2' || key.KeyChar == '3' || key.KeyChar == '4'))
                {
                    if (sb.Length < 1)
                    {
                        sb.Append(key.KeyChar);
                        Console.Write(key.KeyChar.ToString());
                    }
                }
            }

            int result;
            if (sb.Length > 0)
            {
                result = int.Parse(sb.ToString());    
            }                
            else
            {
                result = 0;   
            }              

            return result;
        }
    }
}
