using System;

namespace _42_CUSTOM_SORT_DEMO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var stringArray = new string[] { "abcdi", "dcdef", "aby", "abc", "abcdef", "abcdez" };

            Func<string, string, bool> stringComparer = Compare;

            _41_CUSTOM_SORT.Program.Sort(stringArray, stringComparer);
            _41_CUSTOM_SORT.Program.WriteArray(stringArray);
        }

        private static bool Compare<T>(T x, T y)
        {
            var stringX = x.ToString();
            var stringY = y.ToString();

            if (stringX.Length < stringY.Length)
            {
                return true;
            }

            if (stringX.Length == stringY.Length)
            {
                for (int i = 0; i < stringX.Length; i++)
                {
                    if (stringX[i] < stringY[i])
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
