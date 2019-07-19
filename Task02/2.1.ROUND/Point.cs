using System;

namespace _2._1.ROUND
{
    public class Point
    {
        public int X { get; private set; } = 0;

        public int Y { get; private set; } = 0;

        public Point(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public static Point Create(int X, int Y) => new Point(X, Y);

        public void MoveTo(Point location)
        {
            LocationNullCheck(location);

            X = location.X;
            Y = location.Y;
        }

        public static Point operator +(Point point1, Point point2) => new Point(point1.X + point1.X, point2.Y + point2.Y);

        public static Point operator -(Point point1, Point point2) => new Point(point1.X - point1.X, point2.Y - point2.Y);

        public static bool operator >(Point point1, Point point2) => point1.X + point1.Y > point2.X + point2.Y;

        public static bool operator <(Point point1, Point point2) => point1.X + point1.Y < point2.X + point2.Y;

        public static bool operator ==(Point point1, Point point2) => point1.X == point2.X & point1.Y == point2.Y ? true : false;

        public static bool operator !=(Point point1, Point point2) => point1.X != point2.X || point1.Y != point2.Y ? true : false;

        public override bool Equals(object obj) => obj is Point point && X == point.X && Y == point.Y;

        public override int GetHashCode() => X.GetHashCode() + Y.GetHashCode();

        private void LocationNullCheck(Point location)
        {
            if (location is null)
            {
                throw new ArgumentNullException($"{nameof(location)} is null!");
            }
        }
    }
}
