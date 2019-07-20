using System;
using System.Text;

namespace _32_WORD_FREQUENCY
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Введите английский текст");
            var wordsArray = GetEnglishTextFromConsole().ToLower().Split(new char[] { ' ', '.' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in wordsArray)
            {

            }

        }

        private static string GetEnglishTextFromConsole()
        {
            bool inputComplete = false;
            StringBuilder userKeySB = new StringBuilder();

            while (!inputComplete)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        inputComplete = true;
                        break;

                    case ConsoleKey.Backspace:
                        EmulateConsoleKeyBackSpace(userKeySB);
                        break;
                }

                if ((key.KeyChar >= 'A' & key.KeyChar <= 'Z') || (key.KeyChar >= 'a' & key.KeyChar <= 'z') 
                    || char.IsPunctuation(key.KeyChar) 
                    || char.IsWhiteSpace(key.KeyChar))
                {
                    userKeySB.Append(key.KeyChar);
                    Console.Write(key.KeyChar);
                }
            }

            return userKeySB.Length > 0 ? userKeySB.ToString() : string.Empty;
        }

        static void EmulateConsoleKeyBackSpace(StringBuilder sb)
        {
            if (sb.Length > 0)
            {
                sb.Length--;
            }

            Console.Clear();
            Console.WriteLine("Введите английский текст");
            Console.Write(sb);
        }
    }
}
