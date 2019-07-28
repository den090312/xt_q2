using System;
using System.Collections.Generic;
using System.Linq;

namespace _46_I_SEEK_YOU
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var array = new int[] { -1, 3, 0, -7, -9, 5 };

            //экземпляр делегата
            var condition1 = new Condition<int>(IsPositive);

            var positiveArray1 = ArrayFindAll(array, condition1);

            //делегат в виде анонимного метода
            Condition<int> condition2 = delegate (int x) { return x > 0; };

            var positiveArray2 = ArrayFindAll(array, condition2);

            //делегат в виде лямбда-выражения
            var positiveArray3 = ArrayFindAll(array, x => x > 0);

            //LINQ-выражение
            var positiveArray4 = array.Where(x => x > 0);
        }

        public static bool IsPositive(int x) => x > 0;

        public delegate bool Condition<T>(T x);

        public static bool ArrayTryFind<T>(T[] array, T targetElement, out int index)
        {
            index = 0;
            bool found = false;

            foreach (T element in array)
            {
                if (element.Equals(targetElement))
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
            _41_CUSTOM_SORT.Program.NullCheck(array);
            NullCheck(condition);

            var resultList = new List<T>();

            foreach (T element in array)
            {
                if (condition(element))
                {
                    resultList.Add(element);
                }
            }

            return resultList.ToArray();
        }

        private static void NullCheck<T>(Condition<T> condition)
        {
            if (condition is null)
            {
                throw new ArgumentNullException($"{nameof(condition)} is null!");
            }
        }
    }
}

