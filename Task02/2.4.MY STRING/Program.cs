using System;

namespace _2._4.MY_STRING
{
    class Program
    {
        private static void Main(string[] args)
        {
            //создание объекта через массив
            var myString1 = new MyString(new char[] { 'a', 'b', 'c', 'd', 'e'});

            Console.WriteLine("MyString1:");
            WriteMyString(myString1);

            //создание объекта через строку
            var myString2 = new MyString("43125");

            Console.WriteLine("MyString2:");
            WriteMyString(myString2);

            //конкатенация
            var myString3 = myString1 + myString2;
            Console.WriteLine("MyString1 + MyString2:");
            WriteMyString(myString3);

            Console.WriteLine("Сортировка MyString3:");
            WriteMyString(myString3.Sort());



            //поиск элемента
            var myChar = '3';
            if (myString3.TryFind(myChar, out int index))
            {
                Console.WriteLine($"Значение '{myChar}' найдено, индекс первого вхождения элемента: {index}");
            }
            else
            {
                Console.WriteLine("Значение не найдено");
            }


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
