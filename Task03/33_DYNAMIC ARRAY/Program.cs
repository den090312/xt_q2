using System;
using System.Collections;
using System.Collections.Generic;

namespace _33_DYNAMIC_ARRAY
{
    class Program
    {
        static void Main(string[] args)
        {
            var myList1 = new List<string>() { "0", "1", "3", "3", "5", "6" };
            //var myList1 = new List<string>() { };

            var MyDA = new DynamicArray<string>(myList1);

            //MyDA.Insert("751", 3);
            //MyDA.Remove("2");
            //MyDA.Add("321");

            //var myList2 = new List<string>(new string[6] { "0", "2", "3", "1", "5", "4" });
            //MyDA.AddRange(myList2);

            foreach (string element in MyDA)
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
