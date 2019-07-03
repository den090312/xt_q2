using System;
using System.Text;

namespace Task01Sequence
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetSequence(GetPositiveIntFromConsole()));
        }

        static int GetPositiveIntFromConsole()
        {
            int n;
            bool isInt;
            do
            {
                Console.WriteLine("Введите положительное число меньше 2147483648");
                isInt = int.TryParse(Console.ReadLine(), out n);

                if (isInt)
                {
                    isInt = n > 0;
                }
            }
            while (isInt == false);

            return n;
        }

        static string GetSequence(int n)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= n; i++)
            {
                sb.Append(i);

                if (i != n)
                {
                    sb.Append(", ");
                }
            }

            return sb.ToString();
        }
    }
}
