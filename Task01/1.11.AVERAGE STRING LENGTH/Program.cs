using System;
using System.Text;

namespace _1._11.AVERAGE_STRING_LENGTH
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку. Максимальная длина - 254 символа");
            Console.WriteLine($"Средняя длина слова: {GetAverageStringLength(Console.ReadLine())}");
        }

        static double GetAverageStringLength(string userString)
        {
            //удаляем знаки препинания
            var noPunctuationSB = GetNoPunctuationSB(userString);

            //получаем массив слов
            var wordsArray = noPunctuationSB.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            //удаляем пробелы
            var noSpaceSB = noPunctuationSB.Replace(" ", String.Empty);

            //средняя длина слова равна отношению общего количества символов к количеству слов
            return (double)noSpaceSB.Length / (double)wordsArray.Length;
        }

        static StringBuilder GetNoPunctuationSB(string userString)
        {
            StringBuilder mySB = new StringBuilder();
            var myCharArray = userString.ToCharArray();

            for (int i = 0; i <= myCharArray.Length - 1; i++)
            {
                if (!Char.IsPunctuation(myCharArray[i]))
                {
                    mySB.Append(myCharArray[i]);
                }
            }

            return mySB;
        }
    }
}
