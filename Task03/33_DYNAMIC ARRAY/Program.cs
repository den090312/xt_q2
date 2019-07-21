using System;
using System.Collections.Generic;

namespace _33_DYNAMIC_ARRAY
{
    class Program
    {
        static void Main(string[] args)
        {
            var MyDA = new DynamicArray<string>(new string[6] { "7", "3", "5", "10", "13", "123" });
            //MyDA.Insert("751", 3);

            MyDA.Remove("5");

            for (int i = 0; i < MyDA.Length; i++)
            {
                Console.WriteLine(MyDA[i]);
            }
        }
    }
}
