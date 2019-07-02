using System;
using System.Text;

namespace _111AverageStringLength
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку. Максимальная длина - 254 симвоола");
            Console.WriteLine("");
            Console.WriteLine("Средняя длина слова: " + GetAveradgeStringLength(Console.ReadLine()));
        }

        static double GetAveradgeStringLength(string userString)
        {
            //удаляем все знаки препинания кроме пробелов
            StringBuilder mySB = GetNoPuncExceptSpaceSB(userString);

            //получаем массив слов
            var wordsArray = mySB.ToString().Split(' ');

            //удаляем пробелы
            var noSpaceString = mySB.ToString().Replace(" ", "");

            //средняя длина слова равна отношению количества символов в строке к количеству слов
            return (double)noSpaceString.Length / (double)wordsArray.Length;
        }

        static StringBuilder GetNoPuncExceptSpaceSB(string userString)
        {
            StringBuilder mySB = new StringBuilder();
            var myCharArray = userString.ToCharArray();

            for (int i = 0; i <= myCharArray.Length - 1; i++)
            {
                if (!Char.IsPunctuation(myCharArray[i]) | myCharArray[i] == ' ')
                {
                    mySB.Append(myCharArray[i]);
                }
            }

            return mySB;
        }
    }
}
