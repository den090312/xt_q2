
using System;

namespace _44_NUMBER_ARRAY_SUM
{
    class Program
    {
        static void Main(string[] args)
        {
            var myArray = new float[] { 1, 3, 0, 7, 9, 5 };

            Console.WriteLine(myArray.Sum());
        }
    }

    internal static class NumberArrayExtansion
    {
        public static decimal Sum<T>(this T[] array)
        {
            var newArray = GetDecimalArray(array);

            decimal sum = 0;

            for (int i = 0; i < newArray.Length; i++)
            {
                sum += newArray[i];
            }

            return sum;
        }

        private static decimal[] GetDecimalArray<T>(T[] array)
        {
            var decimalArray = new decimal[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                decimalArray[i] = Convert.ToDecimal(array[i]);
            }

            return decimalArray;
        }
    }
}
