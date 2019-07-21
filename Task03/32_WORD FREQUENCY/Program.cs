using System;
using System.Collections.Generic;
using System.Text;

namespace _32_WORD_FREQUENCY
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Введите английский текст");
            var wordsArray = GetEnglishTextFromConsole().Split(new char[] { ' ', '.' , '\r' }, StringSplitOptions.RemoveEmptyEntries);
            WriteDictionary(GetWordsFrequencyDictionary(GetNoPuctuationWordsList(wordsArray)));
        }

        private static string GetEnglishTextFromConsole()
        {
            var userKeySB = new StringBuilder();

            bool inputComplete = false;

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

        private static void EmulateConsoleKeyBackSpace(StringBuilder sb)
        {
            if (sb.Length > 0)
            {
                sb.Length--;
            }

            Console.Clear();
            Console.WriteLine("Введите английский текст");
            Console.Write(sb);
        }

        private static List<string> GetNoPuctuationWordsList(string[] wordsArray)
        {
            var wordsList = new List<string>();

            for (int i = 0; i < wordsArray.Length; i++)
            {
                var word = GetNoPunctuationWord(wordsArray[i]);

                if (word.Length > 0)
                {
                    wordsList.Add(word);
                }
            }

            return wordsList;
        }

        private static string GetNoPunctuationWord(string word)
        {
            var mySB = new StringBuilder();
            var myCharArray = word.ToCharArray();

            for (int i = 0; i <= myCharArray.Length - 1; i++)
            {
                if (!Char.IsPunctuation(myCharArray[i]))
                {
                    mySB.Append(myCharArray[i]);
                }
            }

            return mySB.ToString();
        }

        private static Dictionary<string, int> GetWordsFrequencyDictionary(List<string> wordsList)
        {
            var wordsDict = new Dictionary<string, int>();

            foreach (string word in wordsList)
            {
                if (!wordsDict.ContainsKey(word))
                {
                    wordsDict.Add(word, GetFrequency(word, wordsList));
                }
            }

            return wordsDict;
        }

        private static int GetFrequency(string word, List<string> wordsList)
        {
            int frequency = 0;

            foreach (string element in wordsList)
            {
                if (element.ToLower() == word.ToLower())
                {
                    frequency++;
                }
            }

            return frequency;
        }

        private static void WriteDictionary(Dictionary<string, int> wordsDict)
        {
            Console.WriteLine();
            Console.WriteLine();

            foreach (KeyValuePair<string, int> kvp in wordsDict)
            {
                Console.WriteLine($"Слово \"{kvp.Key}\" встречается {kvp.Value} раз(а)");
            }
        }
    }
}
