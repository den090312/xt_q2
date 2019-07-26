using System;

namespace _42_CUSTOM_SORT_DEMO
{
    public class Program
    {
        public void Main(string[] args)
        {
            var myArray = new string[] { "abcdi", "dcdef", "aby", "abc", "abcdef", "abcdez" };

            Func<string, string, bool> myComparer = Compare;

            Sort(myArray, myComparer);

            foreach (string item in myArray)
            {
                Console.WriteLine(item);
            }
        }

        private static T[] Sort<T>(T[] array, Func<T, T, bool> compare)
        {
            NullCheck(array);
            NullCheck(compare);

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

        private static void Swap<T>(ref T item1, ref T item2)
        {
            var buffer = item1;
            item1 = item2;
            item2 = buffer;
        }

        private static bool Compare<T>(T x, T y)
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

        private static void NullCheck<T>(T[] array)
        {
            if (array is null)
            {
                throw new ArgumentNullException($"{nameof(array)} is null!");
            }
        }

        private static void NullCheck<T>(Func<T, T, bool> comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException($"{nameof(comparer)} is null!");
            }
        }
    }
}
