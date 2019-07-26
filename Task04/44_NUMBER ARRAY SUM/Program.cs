
using System;

namespace _44_NUMBER_ARRAY_SUM
{
    class Program
    {
        static void Main(string[] args)
        {
            var myArray = new int[] { 1, 3, 0, 7, 9, 5 };

            Func<int[], Type> type = GetType;

            var sum = GetSum.Sum(myArray, type);

            Console.WriteLine(sum);
        }

        public static Type GetType<T>(T[] array) => array.GetType();
    }

    internal static class GetSum
    {
        public static T Sum<T>(T[] array, Func<T[], Type> type)
        {
            if (type is int)
            {
                int sum = 0;

                for (int i = 0; i < array.Length; i++)
                {
                    sum += (int)(object)array[i];
                }

                return sum;
            }

            return array[0];
        }
    }
}
