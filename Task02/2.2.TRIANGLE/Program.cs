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
            private static int SetAboveZeroTriangleSide(int userValue)
            {
                if (userValue <= 0)
                {
                    throw new ArgumentException("Сторона треугольника не может быть меньше или равна нулю!");
                }
                else
                {
                    return userValue;
                }
            }

            private int a;
            public int A
            {
                get
                {
                    return a;
                }
                set
                {
                    a = SetAboveZeroTriangleSide(value);

                    if (a >= b + c | b >= a + c | c >= a + b)
                    {
                        throw new ArgumentException("Треугольник с такими сторонами построить нельзя!");
                    }
                    else
                    {
                        a = value;
                    }
                }
            }

            private int b;
            public int B
            {
                get
                {
                    return b;
                }
                set
                {
                    b = SetAboveZeroTriangleSide(value);

                    if (a >= b + c | b >= a + c | c >= a + b)
                    {
                        throw new ArgumentException("Треугольник с такими сторонами построить нельзя!");
                    }
                    else
                    {
                        b = value;
                    }
                }
            }

            private int c;
            public int C
            {
                get
                {
                    return c;
                }
                set
                {
                    c = SetAboveZeroTriangleSide(value);

                    if (a >= b + c | b >= a + c | c >= a + b)
                    {
                        throw new ArgumentException("Треугольник с такими сторонами построить нельзя!");
                    }
                    else
                    {
                        c = value;
                    }
                }
            }

            public Triangle(int A, int B, int C)
            {
                int a = SetAboveZeroTriangleSide(A);
                int b = SetAboveZeroTriangleSide(B);
                int c = SetAboveZeroTriangleSide(C);

                if (a >= b + c | b >= a + c | c >= a + b)
                {
                    throw new ArgumentException("Треугольник с такими сторонами построить нельзя!");
                }
                else
                {
                    this.a = a;
                    this.b = b;
                    this.c = c;
                }
            }

            public int Perimeter => A + B + C;

            public double Area => Math.Sqrt((A + B + C) / 2 * ((A + B + C) / 2 - A) * ((A + B + C) / 2 - B) * ((A + B + C) / 2 - C));
        }
    }
}
