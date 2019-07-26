using System;

namespace _42_CUSTOM_SORT_DEMO
{
    class Program
    {
        static void Main(string[] args)
        {
            var myArray = new string[] { "123", "3рвпарпв5", "иу5гтрпвв", "п8ыкт94и668768", "34", "-08ыи7па" };

            Sort(myArray, (x, y) => x < y);

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

            if (x.ToString().Length < y.ToString().Length)
            {
                return true;
            }

            if (x.ToString().Length == y.ToString().Length)
            {

            }

            return true;
        }
    }
}
