using System;
using System.Text;

namespace _111AverageStringLength
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку. Максимальная длина - 254 симвоола");
           //StringBuilder myStringBuilder = new StringBuilder(Console.ReadLine());

            //удаляем знаки препинания из строки
            StringBuilder myStringBuilder = GetNoPunctuationSB(Console.ReadLine());

            //копируем SB в новую строку
            var fullString = myStringBuilder.ToString();
            
            //считаем общее количество символов в строке
            var noSpaceStringLength = fullString.Replace(" ", "").Length;

            //получаем массив слов
            var wordsArray = myStringBuilder.ToString().Split(' ');

            //средняя длина слова равна отношению количества символов строки к количеству слов
            var result = (double)noSpaceStringLength / (double)wordsArray.Length;

            Console.WriteLine("Средняя длина слова: "+ result);
        }

        static StringBuilder GetNoPunctuationSB(string userString)
        {
            StringBuilder myStringBuilder = new StringBuilder();
            var myCharArray = userString.ToCharArray();

            for (int i = 0; i <= myCharArray.Length; i++)
            {
                if (!Char.IsPunctuation(myCharArray[i]))
                {
                    myStringBuilder.Append(myCharArray[i]);
                }
            }

            return myStringBuilder;
        }
    }
}
