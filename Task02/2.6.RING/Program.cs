using _2._1.ROUND;
using System;

namespace _2._6.RING
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var ringCenter = new Point
            (
                _1.ROUND.Program.GetCoordinateFromConsole("Центр кольца", 'X'), 
                _1.ROUND.Program.GetCoordinateFromConsole("Центр кольца", 'Y')
            );

            Console.WriteLine("Внутренний");
            var InnerRadius = _1.ROUND.Program.GetRadiusFromConsole();

            var myCircle = new Circle(ringCenter, InnerRadius);

            Console.WriteLine("Внешний");
            var OuterRadius = _1.ROUND.Program.GetRadiusFromConsole();

            //кольцо, созданное из окружности (в ней внутренний радиус) и внешнего радиуса
            var myRing = new Ring(myCircle, OuterRadius);
            WriteRingInfo(myRing);
        }

        private static void WriteRingInfo(Ring myRing)
        {
            Console.WriteLine();
            Console.WriteLine($"Координаты центра: ({myRing.Circle.CenterCoordinates.X},{myRing.Circle.CenterCoordinates.Y})");
            Console.WriteLine($"Внутренний радиус: {myRing.Circle.Radius}");
            Console.WriteLine($"Внешний радиус: {myRing.Radius}");
            Console.WriteLine($"Площадь кольца: {myRing.Area}");
            Console.WriteLine($"Суммарная длина окружностей: {myRing.TotalCircumference}");
            Console.WriteLine();
        }
    }
}
