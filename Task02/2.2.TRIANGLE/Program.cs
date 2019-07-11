using System;

namespace _2._2.TRIANGLE
{
    class Program
    {
        static void Main(string[] args)
        {
            Triangle myTriangle = new Triangle(3, 4, 5);
            WriteTriangleInfo(myTriangle);

            myTriangle.A = 100;
            WriteTriangleInfo(myTriangle);
        }

        static void WriteTriangleInfo(Triangle userTriangle)
        {
            Console.WriteLine();
            Console.WriteLine($"Стороны треугольника: {userTriangle.A}, {userTriangle.B}, {userTriangle.C}");
            Console.WriteLine($"Периметр треугольника: {userTriangle.Perimeter}");
            Console.WriteLine($"Площадь треугольника: {userTriangle.Area}");
        }

        class Triangle
        {
            private int a, b, c;

            public Triangle(int A, int B, int C)
            {
                CheckTriangleSideAboveZero(A);
                CheckTriangleSideAboveZero(B);
                CheckTriangleSideAboveZero(C);

                CheckTriangleExistence(A, B, C);

                a = A;
                b = B;
                c = C;
            }

            public int A
            {
                get => a;
                set => CheckTriangleExistence(GetTrinagleSide(value), b, c);
            }

            public int B
            {
                get => b;
                set => CheckTriangleExistence(a, GetTrinagleSide(value), c);
            }

            public int C
            {
                get => c;
                set => CheckTriangleExistence(a, b, GetTrinagleSide(value));
            }

            public int Perimeter => A + B + C;

            public double Area
            {
                get
                {
                    var halfPerimeter = (A + B + C) / 2;

                    return Math.Sqrt(halfPerimeter * (halfPerimeter - A) * (halfPerimeter - B) * (halfPerimeter - C));
                }
            }

            private int GetTrinagleSide(int sideValue)
            {
                CheckTriangleSideAboveZero(sideValue);

                return sideValue;
            }

            private void CheckTriangleSideAboveZero(int userValue)
            {
                if (userValue <= 0)
                {
                    throw new ArgumentException("Сторона треугольника не может быть меньше или равна нулю!");
                }
            }

            private static void CheckTriangleExistence(int a, int b, int c)
            {
                if (a >= b + c | b >= a + c | c >= a + b)
                {
                    throw new ArgumentException("Треугольник с такими сторонами построить нельзя!");
                }
            }
        }
    }
}
