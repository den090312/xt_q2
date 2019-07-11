using System;

namespace _2._2.TRIANGLE
{
    class Program
    {
        static void Main(string[] args)
        {

            int a = GetTriangleSideFromConsole('A');
            int b = GetTriangleSideFromConsole('B');
            int c = GetTriangleSideFromConsole('C');

            Triangle myTriangle = new Triangle(a, b, c);
            WriteTriangleInfo(myTriangle);
        }

        static void WriteTriangleInfo(Triangle userTriangle)
        {
            Console.WriteLine();
            Console.WriteLine($"Стороны треугольника: {userTriangle.A}, {userTriangle.B}, {userTriangle.C}");
            Console.WriteLine($"Периметр треугольника: {userTriangle.Perimeter}");
            Console.WriteLine($"Площадь треугольника: {userTriangle.Area}");
            Console.WriteLine();
        }

        static int GetTriangleSideFromConsole(char sideLetter)
        {
            Console.WriteLine($"Сторона треугольника {sideLetter}");

            int triangleSide;

            bool isInt;
            do
            {
                Console.WriteLine($"Введите целое положительное число меньше {int.MaxValue}:");

                isInt = int.TryParse(Console.ReadLine(), out triangleSide);

                if (isInt)
                {
                    isInt = triangleSide > 0;
                }
            }
            while (isInt == false);

            return triangleSide;
        }
    }
}
