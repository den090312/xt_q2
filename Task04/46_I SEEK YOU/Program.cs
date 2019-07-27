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


        }

    }

    internal static class SeachInArrayExtansion
    {
        public static bool TryFind<T>(T[] array, T userElement, out int index)
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

        public static bool TryFind<T>(T[] array, T userElement, out int index, Predicate<T> predicate)
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
    }
}
