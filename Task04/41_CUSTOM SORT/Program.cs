
using System;

namespace _41_CUSTOM_SORT
{
    public class Program
    {
        static void Main(string[] args)
        {
            var myArray = new int[] { 1, 3, 0, 7, -90, 123 };

            Sort(myArray, (x, y) => x < y);

            foreach (int item in myArray)
            {
                Console.WriteLine(item);
            }
        }

        public static T[] Sort<T>(T[] array, Func<T, T, bool> compare)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (compare(array[j], array[i]))
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
    }
}
