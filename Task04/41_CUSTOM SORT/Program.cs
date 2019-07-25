
using System;

namespace _41_CUSTOM_SORT
{
    public class Program
    {
        static void Main(string[] args)
        {
            var myArray = new int[] { 1, 3, 0, 7, -90, 123 };
            //var sorter = new Sorter<int>(Sort);

            //sorter(myArray);

            Sort(myArray, Compare<int>(3, 5));

            foreach (int item in myArray)
            {
                Console.WriteLine(item);
            }
        }

        public static T[] Sort<T>(T[] array, Func<T> compareResult)
        {
            return array;
        }

        public static Func<T> Compare<T>(int x, int y)
        {
            if (x > y)
                return 1;
            else if (x < y)
                return -1;
            else
                return 0;
        }
    }
}
