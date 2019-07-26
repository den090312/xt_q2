
using System;

namespace _43_SORTING_UNIT
{
    public static class SortingUnit
    {
        public static event Action<string> SortingIsDoneAlert;

        public static void SortingIsDone(string alertText)
        {
            Console.WriteLine(alertText);
        }
    }
}
