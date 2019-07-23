using System;
using System.Collections;
using System.Collections.Generic;

namespace _33_DYNAMIC_ARRAY
{
    class Program
    {
        static void Main(string[] args)
        {
            var myList1 = new List<string>() { "0", "1", "2", "3", "4", "5" };

            var MyDA = new DynamicArray<string>(myList1);

            //MyDA.Insert("751", 3);
            //MyDA.Remove("2");
            //MyDA.Add("321");

            var myList2 = new List<string>(new string[6] { "6", "7", "8", "9", "10", "11" });
            MyDA.AddRange(myList2);

            foreach (string element in MyDA)
            {
                Console.WriteLine(element);
            }

            Console.WriteLine($"capacity = {MyDA.Capacity}");

            //var cycledArray = new CycledDynamicArray<string>(myList1.ToArray());

            //foreach (string item in cycledArray)
            //{
            //    Console.WriteLine(item);
            //}
        }
    }
}
