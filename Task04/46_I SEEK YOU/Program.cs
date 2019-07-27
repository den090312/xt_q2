using System;
using System.Collections.Generic;
using System.Linq;

namespace _46_I_SEEK_YOU
{
    class Program
    {
        static void Main(string[] args)
        {
            var myArray = new int[] { 1, 3, 0, 7, 9, 5 };

            //экземпляр делегата
            var condition1 = new Condition<int>(IsPositive);

            var positiveArray1 = ArrayFindAll(myArray, condition1);

            //делегат в виде анонимного метода
            Condition<int> condition2 = delegate (int userInt) { return userInt > 0; };

            var positiveArray2 = ArrayFindAll(myArray, condition2);

            //делегат в виде лямбда-выражения
            var positiveArray3 = ArrayFindAll(myArray, x => x > 0);

            //LINQ-выражение
            var positiveArray4 = myArray.Where(x => x > 0);
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

        public static T[] ArrayFindAll<T>(T[] array, Condition<T> condition)
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

