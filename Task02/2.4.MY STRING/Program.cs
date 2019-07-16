using System;

namespace _2._4.MY_STRING
{
    public class Program
    {
        private static void Main(string[] args)
        {
            //создание объекта через массив
            var myString1 = new MyString(new char[] { 'a', 'b', 'c', 'd', 'e', 'i', 'j'});

            Console.WriteLine("MyString1:");
            WriteMyString(myString1);

            //создание объекта через строку
            var myString2 = new MyString("46371825");

            Console.WriteLine("MyString2:");
            WriteMyString(myString2);

            //конкатенация
            var myString3 = myString1 + myString2;
            Console.WriteLine("MyString1 + MyString2:");
            WriteMyString(myString3);

            //сортировка
            Console.WriteLine("Сортировка MyString3:");
            WriteMyString(myString3.Sort());

            //реверс
            Console.WriteLine("Обратный MyString3:");
            WriteMyString(myString3.Reverse());

            //поиск элемента
            Console.WriteLine("Поиск в MyString3:");
            var myChar = '3';

            if (myString3.TryFind(myChar, out int index))
            {
                Console.WriteLine($"Значение '{myChar}' найдено, индекс первого вхождения элемента: {index}");
            }
            else
            {
                Console.WriteLine("Значение не найдено");
            }

            Console.WriteLine();

            //получаем значение по индексу
            Console.WriteLine("Индексатор:");
            Console.WriteLine($"MyString3[0] = '{myString3[0]}'");
            Console.WriteLine();

            //получаем длину массива
            Console.WriteLine("Длина MyString3:");
            Console.WriteLine(myString3.Length);
            Console.WriteLine();
        }

        private static void WriteMyString(MyString myString)
        {
            var charArray = myString.ToCharArray();

            foreach (char element in charArray)
            {
                Console.Write($"{element}");
            }

            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
