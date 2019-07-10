using System;

namespace _2._2.TRIANGLE
{
    class Program
    {
        static void Main(string[] args)
        {
            Triangle myTriangle = new Triangle(3, 4, 5);

            Console.WriteLine($"Стороны треугольника: {myTriangle.A}, {myTriangle.B}, {myTriangle.C}");
            Console.WriteLine($"Периметр треугольника: {myTriangle.Perimeter}");
            Console.WriteLine($"Площадь треугольника: {myTriangle.Area}");
        }

        class Triangle
        {
            public int A { get; }
            public int B { get; }
            public int C { get; }

            public int Perimeter => A + B + C;

            public double Area => Math.Sqrt((A + B + C) / 2 * ((A + B + C) / 2 - A) * ((A + B + C) / 2 - B) * ((A + B + C) / 2 - C));

            public Triangle(int A, int B, int C)
            {
                if (A <= 0)
                    ThrowException();
                else
                    this.A = A;

                if (B <= 0)
                    ThrowException();
                else
                    this.B = B;

                if (C <= 0)
                    ThrowException();
                else
                    this.C = C;
            }

            private void ThrowException() => throw new ArgumentException("Сторона треугольника не может быть меньше или равно нулю!");
        }
    }
}
