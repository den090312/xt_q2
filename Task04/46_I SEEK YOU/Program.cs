using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _46_I_SEEK_YOU
{
    class Program
    {
        static void Main(string[] args)
        {
            var myArray = new int[] { 1, 3, 0, 7, 9, 5 };

            Predicate<int> allPositive = delegate (int x) { return x > 0; };
        }
    }

    internal static class SeachInArrayExtansion
    {
        public static bool ArrayTryFind<T>(T[] array, T userElement, out int index)
        {
            index = 0;
            bool found = false;

            foreach (T element in array)
            {
                if (element.Equals(userElement))
                {
                    return true;
                }
                else
                {
                    index++;
                }
            }

            return found;
        }

        public delegate bool Predicate<in T>(T obj);

        public static T[] ArrayFindAll<T>(T[] array, Predicate<T> predicate)
        {
            var tempList = new List<T>();

            foreach (T element in array)
            {
                if (predicate(element))
                {
                    tempList.Add(element);
                }
            }

            return tempList.ToArray();
        }
    }
}
