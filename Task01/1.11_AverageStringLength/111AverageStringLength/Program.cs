using System;
using System.Text;

namespace _111AverageStringLength
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку. Максимальная длина - 254 символа");
            Console.WriteLine($"Средняя длина слова: {GetAveradgeStringLength(Console.ReadLine())}");
        }

        static double GetAveradgeStringLength(string userString)
        {
            //удаляем из строки все знаки препинания кроме пробелов
            var noPunctuationString = GetNoPuncExceptSpaceString(userString);

            //получаем массив слов
            var wordsArray = noPunctuationString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            //удаляем пробелы из строки
            var noSpaceString = noPunctuationString.Replace(" ", "");

            //средняя длина слова равна отношению количества символов в строке к количеству слов
            return (double)noSpaceString.Length / (double)wordsArray.Length;
        }

        static string GetNoPuncExceptSpaceString(string userString)
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

            //убираем пробелы в начале и в конце строки
            return mySB.ToString().Trim(' ');
        }
    }
}
