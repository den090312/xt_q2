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
            private int a;
            public int A
            {
                get
                {
                    return a;
                }
                set
                {
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
                int a;
                if (A <= 0)
                {
                    throw new ArgumentException("Сторона треугольника не может быть меньше или равно нулю!");
                }
                else
                {
                    a = A;
                }

                int b;
                if (B <= 0)
                {
                    throw new ArgumentException("Сторона треугольника не может быть меньше или равно нулю!");
                }
                else
                {
                    b = B;
                }

                int c;
                if (C <= 0)
                {
                    throw new ArgumentException("Сторона треугольника не может быть меньше или равно нулю!");
                }
                else
                {
                    c = C;
                }

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
