using System;

namespace Task02Simple
{
    class Program
    {
        static void Main(string[] args)
        {
            if (ItsSimpleInt(GetPositiveIntFromConsole()))
                Console.WriteLine("Простое число");
            else
                Console.WriteLine("Составное число");
        }

        static int GetPositiveIntFromConsole()
        {
            int n;
            bool isInt;
            do
            {
                Console.WriteLine("Введите положительное число больше 1 и меньше 2147483647");
                isInt = int.TryParse(Console.ReadLine(), out n);

                if (isInt)
                {
                    isInt = n > 1;
                }
            }
            while (isInt == false);

            return n;
        }

        static bool ItsSimpleInt(int n)
        {
            //четное число - всегда составное (кроме 2)
            if (n % 2 == 0) 
            {
                return n == 2;    
            }                

            //если можно извлечь кв. корень без остатка - число составное
            double dSqrt = Math.Sqrt(n);
            if (int.TryParse(dSqrt.ToString(), out int X)) 
            {
                return false;    
            }                

            //особенности округления при конвертации double в int - к положительным числам прибавить 0.5
            //должно работать быстрее, чем Math.Round() и Convert.ToInt32()
            int M = (int)(dSqrt + 0.5);
            //int N = DoubleToIntRound(dSqrt);

            //проверяем результат от деления на нечетные числа от 3 до округленного кв. корня
            for (int i = 3; i <= M; i += 2)
            {
                if (n % i == 0)
                {
                    return false;  
                }
            }

            return true;
        }

        //метод округления при конвертации double в int
        //использовать не стал, потому что отрицательные числа отсекаются на этапе проверки ввода
        static int DoubleToIntRound(double d) => d < 0 ? (int)(d - 0.5) : (int)(d + 0.5);
        /*{
            if (d < 0)
            {
                return (int)(d - 0.5);
            }
            return (int)(d + 0.5);
        }*/
    }
}
