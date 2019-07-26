﻿
using System;

namespace _44_NUMBER_ARRAY_SUM
{
    class Program
    {
        static void Main(string[] args)
        {
            var myArray = new int[] { 1, 3, 0, 7, 9, 5 };

            Func<int[], Type> type = GetType;

            Console.WriteLine(GetSum.Sum(myArray, type));
        }

        public static Type GetType<T>(T[] array) => array.GetType();
    }

    internal static class GetSum
    {
        public static T Sum<T>(T[] array, Func<T[], Type> type)
        {
            var arrayType = array.GetType();

            T sum = 0;

            foreach (T item in array)
            {
                sum += item;
            }

            return sum;
        }
    }
}
