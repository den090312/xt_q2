
using System;

namespace _44_NUMBER_ARRAY_SUM
{
    class Program
    {
        static void Main(string[] args)
        {
            var myArray = new int[] { 1, 3, 0, 7, 9, 5 };

            Func<int[], decimal[]> toDecimalArray = ToDecimalArray;

            var sum = GetSum.Sum((decimal[])(object)myArray);

            Console.WriteLine(sum);
        }

        public static decimal[] ToDecimalArray<T>(T array) => (decimal[])(object)array;
    }

    internal static class GetSum
    {
        public static decimal Sum(decimal[] array)
        {
            decimal sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }

            return sum;
        }
    }
}
