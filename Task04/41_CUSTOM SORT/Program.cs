
using System;

namespace _41_CUSTOM_SORT
{
    public class Program
    {
        static void Main(string[] args)
        {
            var myArray = new string[]{ "1", "3", "0", "7", "-90", "123" };

            Sorter<string> sorter = new Sorter<string>(Sort);

            sorter(myArray);
        }

        public delegate T[] Sorter<T>(T[] array);

        public static T[] Sort<T>(T[] array)
        {
            Array.Sort(array);

            return array;
        }
    }
}
