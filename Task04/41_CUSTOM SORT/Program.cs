
using System;

namespace _41_CUSTOM_SORT
{
    public class Program
    {
        static void Main(string[] args)
        {
            var myArray = new int[] { 1, 3, 0, 7, -90, 123 };

            Sort(myArray);

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
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (Compare(array[j], array[i]) < 0)
                    {
                        Swap(ref array[i], ref array[j]);
                    }
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
            if ((int)(object)x > (int)(object)y)
                return 1;
            else if ((int)(object)x < (int)(object)y)
                return -1;
            else
                return 0;
        }
    }
}
