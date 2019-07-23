using System;
using System.Collections;
using System.Collections.Generic;

namespace _33_DYNAMIC_ARRAY
{
    class Program
    {
        static void Main(string[] args)
        {
            var myList1 = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" };

            var MyDA = new DynamicArray<string>(myList1);

            //MyDA.Insert("751", 15);
            //MyDA.Remove("2");
            //MyDA.Add("321");

            //var myList2 = new List<string>(new string[6] { "6", "7", "8", "9", "10", "11" });
            //MyDA.AddRange(myList2);

            foreach (string element in MyDA)
            {
                Console.WriteLine(element);
            }

            Console.WriteLine($"capacity = {MyDA.Capacity}");
            Console.WriteLine($"length = {MyDA.Length}");

            //Console.WriteLine($"MyDA[-3] = {MyDA[-3]}");

            Console.WriteLine();

            MyDA.Capacity = 5;

            foreach (string element in MyDA)
            {
                Console.WriteLine(element);
            }

            Console.WriteLine($"capacity = {MyDA.Capacity}");
            Console.WriteLine($"length = {MyDA.Length}");

            Console.WriteLine();
            Console.WriteLine("Клон:");

            var MyDaClone = (DynamicArray<string>)MyDA.Clone();

            foreach (string element in MyDaClone)
            {
                Console.WriteLine(element);
            }

            Console.WriteLine();
            Console.WriteLine("Массив:");

            var array = MyDaClone.ToArray();

            foreach (string element in array)
            {
                Console.WriteLine(element);
            }

            //var cycledArray = new CycledDynamicArray<string>(myList1.ToArray());

            //foreach (string item in cycledArray)
            //{
            //    Console.WriteLine(item);
            //}
        }
    }
}
