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
                set
                {
                    int userValue = GetTrinagleSide(value);
                    CheckTriangleExistence(userValue, b, c);
                    a = userValue;
                }
            }

            public int B
            {
                get => b;
                set
                {
                    int userValue = GetTrinagleSide(value);
                    CheckTriangleExistence(a, userValue, c);
                    b = userValue;
                }
            }

            public int C
            {
                get => c;
                set
                {
                    int userValue = GetTrinagleSide(value);
                    CheckTriangleExistence(a, b, userValue);
                    c = userValue;
                }
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
