namespace _2._1.ROUND
{
    public class Point
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Point(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public static Point operator +(Point point1, Point point2) => new Point(point1.X + point1.X, point2.Y + point2.Y);

        public static Point operator -(Point point1, Point point2) => new Point(point1.X - point1.X, point2.Y - point2.Y);

        public static bool operator >(Point point1, Point point2) => point1.X + point1.Y > point2.X + point2.Y;

        public static bool operator <(Point point1, Point point2) => point1.X + point1.Y < point2.X + point2.Y;

        public static bool operator ==(Point point1, Point point2) => point1.X == point2.X & point1.Y == point2.Y ? true : false;

        public static bool operator !=(Point point1, Point point2) => point1.X != point2.X || point1.Y != point2.Y ? true : false;

        public override bool Equals(object obj) => obj is Point point && X == point.X && Y == point.Y;

        public override int GetHashCode() => X.GetHashCode() + Y.GetHashCode();
    }
}
