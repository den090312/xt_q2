using System;

namespace _42_CUSTOM_SORT_DEMO
{
    class Program
    {
        static void Main(string[] args)
        {
            var myArray = new string[] { "abcdi", "dcdef", "aby", "abc", "abcdef", "abcdez" };

            Func<string, string, bool> myComparer = Compare;

            Sort(myArray, myComparer);

            foreach (string item in myArray)
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

        public static bool Compare<T>(T x, T y)
        {
            var stringX = x.ToString();
            var stringY = y.ToString();

            if (stringX.Length < stringY.Length)
            {
                return true;
            }

            if (stringX.Length == stringY.Length)
            {
                for (int i = 0; i < stringX.Length; i++)
                {
                    if (stringX[i] < stringY[i])
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
