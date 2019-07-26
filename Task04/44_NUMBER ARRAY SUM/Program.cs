
using System;

namespace _44_NUMBER_ARRAY_SUM
{
    class Program
    {
        static void Main(string[] args)
        {
            var myArray = new int[] { 1, 3, 0, 7, 9, 5 };

            Console.WriteLine(GetSum.Sum(myArray));
        }
    }

    internal static class GetSum
    {
        public static T Sum<T>(T[] array)
        {
            var arrayType = array.GetType();

            T sum;

            foreach (T item in array)
            {
                sum += item;
            }

            return sum;
        }
    }
}
