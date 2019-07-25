
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
            for (int i = 0; i < array.Length; i++)
            {
                if (Compare(array[i], array[i + 1]) == 1)
                {
                    Swap(ref array[i], ref array[i + 1]);
                }
            }

            return array;
        }

        public static void Swap<T>(ref T item1, ref T item2)
        {
            var buffer = item1;
            item1 = item2;
            item2 = buffer;
        }

        public static int Compare<T1, T2>(T1 x, T2 y)
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
