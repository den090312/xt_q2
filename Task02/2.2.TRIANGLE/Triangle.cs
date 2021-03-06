﻿using System;

namespace _2._2.TRIANGLE
{
    public class Triangle
    {
        private int a, b, c;

        public Triangle(int sideA, int sideB, int sideC)
        {
            CheckTriangleSideAboveZero(sideA);
            CheckTriangleSideAboveZero(sideB);
            CheckTriangleSideAboveZero(sideC);

            CheckTriangleExistence(sideA, sideB, sideC);

            A = sideA;
            B = sideB;
            C = sideC;
        }

        public int A
        {
            get => a;
            set
            {
                CheckTriangleSideAboveZero(value);
                CheckTriangleExistence(value, b, c);
                a = value;
            }
        }

        public int B
        {
            get => b;
            set
            {
                CheckTriangleSideAboveZero(value);
                CheckTriangleExistence(a, value, c);
                b = value;
            }
        }

        public int C
        {
            get => c;
            set
            {
                CheckTriangleSideAboveZero(value);
                CheckTriangleExistence(a, b, value);
                c = value;
            }
        }

        public int Perimeter => A + B + C;

        public double Area
        {
            get
            {
                var halfPerimeter = Perimeter / 2;

                return Math.Sqrt(halfPerimeter * (halfPerimeter - A) * (halfPerimeter - B) * (halfPerimeter - C));
            }
        }

        private void CheckTriangleSideAboveZero(int userValue)
        {
            if (userValue <= 0)
            {
                throw new ArgumentException("Сторона треугольника должна быть больше нуля!");
            }
        }

        private static void CheckTriangleExistence(int a, int b, int c)
        {
            if (a >= b + c || b >= a + c || c >= a + b)
            {
                throw new ArgumentException("Треугольник с заданными сторонами не существует!");
            }
        }
    }
}
