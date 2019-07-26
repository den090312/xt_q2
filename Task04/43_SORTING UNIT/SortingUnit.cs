
using System;
using System.Threading;
using _41_CUSTOM_SORT;

namespace _43_SORTING_UNIT
{
    public static class SortingUnit
    {
        public static event Action<string> SortingIsDoneAlert;

        public static void SortingIsDone(string alertText)
        {
            Console.WriteLine(alertText);
        }

        public static void ThreadProc()
        {
        }

        public static void SortingInThread<T>(T[] array, Func<T, T, bool> compare)
        {
            var thread = new Thread(new ThreadStart(ThreadProc));

            thread.Start();

            var threadState = $"Thread {Thread.CurrentThread.ManagedThreadId} {Thread.CurrentThread.ThreadState}";

            Console.WriteLine($"{threadState}");

            _41_CUSTOM_SORT.Program.Sort(array, compare);

            Console.WriteLine($"{threadState}");

            SortingIsDoneAlert?.Invoke($"{threadState}");

            Console.WriteLine();
            Console.WriteLine($"{threadState} result array");

            foreach (T item in array)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
        }
    }
}
