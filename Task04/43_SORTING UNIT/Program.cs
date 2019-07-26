using _41_CUSTOM_SORT;
using System;

namespace _43_SORTING_UNIT
{
    class Program
    {
        static void Main(string[] args)
        {
            SortingUnit.SortingIsDoneAlert += SortingUnit.SortingIsDone;

            var myArray = new int[] { 1, 3, 0, 7, 9, 5 };

            SortingUnit.SortingInThread(myArray, (x, y) => x < y);
            SortingUnit.SortingInThread(myArray, (x, y) => x < y);
            SortingUnit.SortingInThread(myArray, (x, y) => x < y);
        }
    }
}
