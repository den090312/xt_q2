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

            Func<int, bool> condition = IsPositive;

            var allPositiveArray1 = SeachInArrayExtansion.ArrayFindAll(myArray, condition);

            var allPositiveArray2 = SeachInArrayExtansion.ArrayFindAll(myArray, x => x > 0);
        }

        public static bool IsPositive(int userInt) => userInt > 0;
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

        public static T[] ArrayFindAll<T>(T[] array, Func<T, bool> condition)
        {
            var tempList = new List<T>();

            foreach (T element in array)
            {
                if (condition(element))
                {
                    tempList.Add(element);
                }
            }

            return tempList.ToArray();
        }
    }
}
