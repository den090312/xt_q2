
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

            foreach (int item in myArray)
            {
                Console.WriteLine(item);
            }
        }

        public delegate MyComparer Func<in T1, in T2, out MyComparer>(T1 arg1, T2 arg2);

        public static T[] Sort<T>(T[] array)
        {
            return array;
        }

        public static int Compare<T1, T2>(int x, int y)
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
