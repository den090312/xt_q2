
using System;
using System.Threading;
using _41_CUSTOM_SORT;

namespace _43_SORTING_UNIT
{
    public static class SortingUnit
    {
        public static event Action<string> SortingIsDoneAlert;

        public static void SortingIsDone(string alertText) => Console.WriteLine(alertText);

        public static void SortingInThread<T>(T[] array, Func<T, T, bool> compare)
        {
            var thread = new Thread(() =>
            {
                var threadId = $"Thread {Thread.CurrentThread.ManagedThreadId}";

                Console.WriteLine($"{threadId}: start");

                _41_CUSTOM_SORT.Program.Sort(array, compare);

                Console.WriteLine($"{threadId}: in progress");

                SortingIsDoneAlert?.Invoke($"{threadId}: finish");

                Console.WriteLine();
                Console.WriteLine($"{threadId} result array:");

                _41_CUSTOM_SORT.Program.WriteArray(array);

                Console.WriteLine();
            });

            thread.Start();
        }
    }
}
