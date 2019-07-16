﻿using System;

namespace _2._1.ROUND
{
    public class Circle
    {
        private Point edgeCoordinates = new Point(0, 0);
        private Point centerCoordinates = new Point(0, 0);
        private double radius = 0;

        //конструктор через точки
        public Circle(Point userCenterCoordinates, Point userEdgeCoordinates)
        {
            CheckCoordinates(userCenterCoordinates, userEdgeCoordinates);
            CenterCoordinates = userCenterCoordinates;
            EdgeCoordinates = userEdgeCoordinates;
        }

        //конструктор с указанием радиуса
        public Circle(Point userCenterCoordinates, double userRadius)
        {
            if (userRadius <= 0)
            {
                throw new Exception("Радиус должен быть больше или равен нулю!");
            }

            centerCoordinates = userCenterCoordinates;
            radius = userRadius;
        }

        public Point CenterCoordinates
        {
            get => centerCoordinates;
            private set
            {
                CheckCoordinates(centerCoordinates, edgeCoordinates);
                centerCoordinates = value;
            }
        }

        public Point EdgeCoordinates
        {
            get => edgeCoordinates;
            private set => edgeCoordinates = value;
        }

        public double Radius
        {
            get
            {
                if (radius == 0)
                {
                    return Math.Sqrt
                    (
                        Math.Pow(edgeCoordinates.X - centerCoordinates.X, 2) +
                        Math.Pow(edgeCoordinates.Y - centerCoordinates.Y, 2)
                    );
                }
                else
                {
                    return radius;
                }
            }
            private set => radius = value;
        }

        public double Circumference => 2 * Math.PI * Radius;

        private void CheckCoordinates(Point userCenterCoordinates, Point userEdgeCoordinates)
        {
            if (userEdgeCoordinates == userCenterCoordinates)
            {
                throw new Exception("Координаты края окружности не могут быть равны координатам центра!");
            }
        }

        private void MoveCircle(Point newCenterCoordinates)
        {
            edgeCoordinates += newCenterCoordinates - centerCoordinates;
        }
    }
}
