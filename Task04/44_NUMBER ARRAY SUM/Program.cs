
using System;

namespace _44_NUMBER_ARRAY_SUM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var myArray = new char[] { '1', '3', '0', '7', '9', '5' };
            //var myArray = new float[] { 1, 3, 0, 7, 9, 5 };
            //var myArray = new double[] { 1, 3, 0, 7, 9, 5 };
            //var myArray = new int[] { 1, 3, 0, 7, 9, 5 };
            var myArray = new long[] { 1, 3, 0, 7, 9, 5 };

            Console.WriteLine(myArray.Sum());
        }
    }

    public static class NumberArrayExtansion
    {
        public static T Sum<T>(this T[] array)
        {
            NumberArrayCheck(array);

            decimal sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                sum += Convert.ToDecimal(array[i]);
            }

            return (T)Convert.ChangeType(sum, typeof(T));
        }

        private static void NumberArrayCheck<T>(T[] array)
        {
            try
            {
                var tryPar = Convert.ToDecimal(array[0]);
            }
            catch
            {
                throw new ArgumentException($"'{nameof(array)}' is not a number array!");
            }
        }
    }
}
