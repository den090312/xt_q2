using System;

namespace _41_CUSTOM_SORT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var myArray = new int[] { 1, 3, 0, 7, 9, 5 };

            Sort(myArray, (x, y) => x < y);

            WriteArray(myArray);
        }

        public static void WriteArray<T>(T[] myArray)
        {
            foreach (T item in myArray)
            {
                Console.WriteLine(item);
            }
        }

        public static T[] Sort<T>(T[] array, Func<T, T, bool> comparer)
        {
            NullCheck(array);
            NullCheck(comparer);

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (comparer(array[j], array[i]))
                    {
                        Swap(ref array[i], ref array[j]);
                    }
                }
            }

            return array;
        }

        private static void Swap<T>(ref T item1, ref T item2)
        {
            var buffer = item1;
            item1 = item2;
            item2 = buffer;
        }

        public static void NullCheck<T>(T[] array)
        {
            if (array is null)
            {
                throw new ArgumentNullException($"{nameof(array)} is null!");
            }
        }

        public static void NullCheck<T>(Func<T, T, bool> comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException($"{nameof(comparer)} is null!");
            }
        }
    }
}
