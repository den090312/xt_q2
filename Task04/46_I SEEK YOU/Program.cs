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
    }
}
