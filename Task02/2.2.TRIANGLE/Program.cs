using System;

namespace _2._2.TRIANGLE
{
    class Program
    {
        static void Main(string[] args)
        {
            Triangle myTriangle = new Triangle(3, 4, 5);
            WriteTriangleInfo(myTriangle);

            myTriangle.A = -4;
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
                int a = SetAboveZeroTriangleSide(A);
                int b = SetAboveZeroTriangleSide(B);
                int c = SetAboveZeroTriangleSide(C);

                CheckTriangleExistence(a, b, c);

                this.a = a;
                this.b = b;
                this.c = c;
            }

            public int A
            {
                get => a;
                set
                {
                    a = SetAboveZeroTriangleSide(value);
                    CheckTriangleExistence(a, b, c);
                    a = value;
                }
            }

            public int B
            {
                get => b;
                set
                {
                    b = SetAboveZeroTriangleSide(value);
                    CheckTriangleExistence(a, b, c);
                    b = value;
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

            public int C
            {
                get => c;
                set
                {
                    c = SetAboveZeroTriangleSide(value);
                    CheckTriangleExistence(a, b, c);
                    c = value;
                }
            }

            private static int SetAboveZeroTriangleSide(int userValue)
            {
                if (userValue <= 0)
                {
                    throw new ArgumentException("Сторона треугольника не может быть меньше или равна нулю!");
                }

                return userValue;
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
