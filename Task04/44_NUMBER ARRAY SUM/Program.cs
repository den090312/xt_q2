
using System;

namespace _44_NUMBER_ARRAY_SUM
{
    class Program
    {
        static void Main(string[] args)
        {
            var myArray = new int[] { 1, 3, 0, 7, 9, 5 };

            Func<int[], decimal[]> decimalArray = GetDecimalArray;

            var sum = GetSum.Sum(myArray, decimalArray);

            Console.WriteLine(sum);
        }

        public static decimal[] GetDecimalArray<T>(T array) => (decimal[])(object)array;
    }

    internal static class GetSum
    {
        public static decimal Sum<T>(T[] array, Func<T[], decimal[]> getDecimalArray)
        {
            var newArray = getDecimalArray(array);

            decimal sum = 0;

            for (int i = 0; i < newArray.Length; i++)
            {
                sum += newArray[i];
            }

            return sum;
        }
    }
}
