using System;

namespace _2._2.TRIANGLE
{
    class Program
    {
        static void Main(string[] args)
        {
            Triangle myTriangle = new Triangle(3, 4, 5);
            WriteTriangleInfo(myTriangle);

            myTriangle.A = 4;
            WriteTriangleInfo(myTriangle);
        }

        static void WriteTriangleInfo(Triangle userTriangle)
        {
            Console.WriteLine($"Стороны треугольника: {userTriangle.A}, {userTriangle.B}, {userTriangle.C}");
            Console.WriteLine($"Периметр треугольника: {userTriangle.Perimeter}");
            Console.WriteLine($"Площадь треугольника: {userTriangle.Area}");
            Console.WriteLine();
        }
    }
}
