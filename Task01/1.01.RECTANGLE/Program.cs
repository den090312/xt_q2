using System;
using System.Text;

namespace _1._1.RECTANGLE
{
    class Program
    {
        static void Main(string[] args)
        {
            int sideIndex; //индекс стороны прямоугольника
            int aValue;    //значение стороны А

            Console.WriteLine("Сторона А:");
            int a = GetIntFromConsole(sideIndex = 1, aValue = 0);

            Console.WriteLine();

            Console.WriteLine("Сторона B:");
            int b = GetIntFromConsole(sideIndex = 2, aValue = a);

            Console.WriteLine();
            Console.WriteLine($"Площадь прямоугольника: {GetSquare(a, b)}");
        }

        static int GetSquare(int a, int b) => a * b;

        static bool InputCompleteFalse(string messageToUser)
        {
            Console.WriteLine("");
            Console.WriteLine(messageToUser);

            return false;
        }

        static bool InputConsoleKeyEnter(StringBuilder sb, bool inputComplete, out int result)
        {
            if (int.TryParse(sb.ToString(), out result))
            {
                if (result == 0)
                {
                    inputComplete = InputCompleteFalse("Ноль - недопустимое значение!");
                    sb.Clear();
                }
                else if (result < 0)
                {
                    inputComplete = InputCompleteFalse("Отрицательное число - недопустимое значение!");
                    sb.Clear();
                }
                else
                {
                    inputComplete = true;
                }
            }
            else
            {
                inputComplete = InputCompleteFalse($"Введенное число превышает {int.MaxValue}");
                sb.Clear();
            }

            return inputComplete;
        }

        static void EmulateConsoleKeyBackSpace(StringBuilder sb, int sideNumber, int aValue)
        {
            //уменьшаем строку на 1 символ
            if (sb.Length > 0)
            {
                sb.Length--;
            }

            //очищаем текущий ввод
            Console.Clear();

            //восстанавливаем предыдущие данные консоли
            if (sideNumber == 1)
            {
                Console.WriteLine("Сторона A:");
            }
            else
            {
                Console.WriteLine("Сторона A:");
                Console.WriteLine(aValue);
                Console.WriteLine("Сторона B:");
            }

            //отображаем строку
            Console.Write(sb);
        }

        static int GetIntFromConsole(int sideIndex, int aValue)
        {
            bool inputComplete = false;
            StringBuilder sb = new StringBuilder();
            int result = 0;

            while (!inputComplete)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    //эмуляция перехода на новую строку 
                    case ConsoleKey.Enter:
                        inputComplete = InputConsoleKeyEnter(sb, inputComplete, out result);
                        break;

                    //эмуляция кнопки Backspace 
                    case ConsoleKey.Backspace:
                        EmulateConsoleKeyBackSpace(sb, sideIndex, aValue);
                        break;
                }

                //присоединяем введенную цифру к строке
                if (char.IsDigit(key.KeyChar))
                {
                    sb.Append(key.KeyChar);
                    Console.Write(key.KeyChar);
                }

                //проверка на знак 'минус'
                if (key.KeyChar == '-')
                {
                    if (!sb.ToString().Contains("-"))
                    {
                        sb.Append(key.KeyChar);
                        Console.Write(key.KeyChar);
                    }
                }
            }

            return result;
        }
    }
}
