﻿using System;
using System.Collections.Generic;

namespace _33_DYNAMIC_ARRAY
{
    class Program
    {
        static void Main(string[] args)
        {
            var myList1 = new List<string>() { };

            //var MyDA = new DynamicArray<string>(-7);

            var MyDA = new DynamicArray<string>(myList1);

            //var newArray = MyDA.ToArray();

            MyDA.Add("321");
            MyDA.Insert("751", 0);
            MyDA.Remove("751");
            MyDA.Remove("321");
            MyDA.Add("01");

            //var myList2 = new List<string>(new string[6] { "6", "7", "8", "9", "10", "11" });
            //MyDA.AddRange(myList2);

            foreach (string element in MyDA)
            {
                Console.WriteLine(element);
            }

            //Console.WriteLine($"capacity = {MyDA.Capacity}");
            //Console.WriteLine($"length = {MyDA.Length}");

            //Console.WriteLine($"MyDA[-3] = {MyDA[-3]}");

            //Console.WriteLine();

            //Console.WriteLine();
            //Console.WriteLine("Обрезка:");

            //MyDA.Capacity = 5;

            //foreach (string element in MyDA)
            //{
            //    Console.WriteLine(element);
            //}

            //Console.WriteLine($"capacity = {MyDA.Capacity}");
            //Console.WriteLine($"length = {MyDA.Length}");

            //Console.WriteLine();
            //Console.WriteLine("Массив:");

            //var array = MyDA.ToArray();

            //foreach (string element in array)
            //{
            //    Console.WriteLine(element);
            //}

            //Console.WriteLine($"capacity = {MyDA.Capacity}");
            //Console.WriteLine($"length = {MyDA.Length}");
            
            //Console.WriteLine();
            //Console.WriteLine("Клон:");

            //var MyDaClone = (DynamicArray<string>)MyDA.Clone();

            //foreach (string element in MyDaClone)
            //{
            //    Console.WriteLine(element);
            //}

            //Console.WriteLine($"capacity = {MyDA.Capacity}");
            //Console.WriteLine($"length = {MyDA.Length}");

            //Console.WriteLine();
            //Console.WriteLine("Циклический массив:");

            //var cycledArray = new CycledDynamicArray<string>(myList1.ToArray());

            //foreach (string item in cycledArray)
            //{
            //    Console.WriteLine(item);
            //}
        }
    }
}
