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

            var allPositiveArray1 = ArrayFindAllFunc(myArray, condition);

            Condition<int> positive = delegate (int userInt) { return userInt > 0; };

            var allPositiveArray2 = ArrayFindAllAnon(myArray, positive);

            var allPositiveArray3 = ArrayFindAllFunc(myArray, x => x > 0);
        }

        public static bool IsPositive(int userInt) => userInt > 0;

        public delegate bool Condition<T>(T userPar);

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

        public static T[] ArrayFindAllFunc<T>(T[] array, Func<T, bool> condition)
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

        public static T[] ArrayFindAllAnon<T>(T[] array, Condition<T> condition)
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

