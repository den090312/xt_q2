
using System;

namespace _41_CUSTOM_SORT
{
    public class Program
    {
        static void Main(string[] args)
        {
            var myArray = new int[]{ 1, 3, 0, 7, -90, 123 };
            var sorter = new Sorter<int>(Sort);

            sorter(myArray);

            foreach (int item in myArray)
            {
                Console.WriteLine(item);
            }
        }

        public delegate T[] Sorter<T>(T[] array);

        public static T[] Sort<T>(T[] array)
        {
            Array.Sort(array);

            return array;
        }
    }
}
