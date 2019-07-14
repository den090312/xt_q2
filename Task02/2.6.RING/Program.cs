using _2._1.ROUND;
using System;

namespace _2._6.RING
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var myCircle = new Circle
            (
                new Point(_1.ROUND.Program.GetCoordinateFromConsole("Центр круга", 'X'), _1.ROUND.Program.GetCoordinateFromConsole("Центр круга", 'Y')),
                new Point(_1.ROUND.Program.GetCoordinateFromConsole("Край круга", 'X'), _1.ROUND.Program.GetCoordinateFromConsole("Край круга", 'Y'))
            );

            var innerRadius = _2._1.ROUND.Program.GetRadiusFromConsole();

            //кольцо, созданное из окружности (в ней внутренний радиус) и внешнего радиуса
            WriteRingInfo(new Ring(myCircle, innerRadius));
        }
        private static void WriteRingInfo(Ring myRing)
        {
            Console.WriteLine();
            Console.WriteLine($"Координаты центра: ({myRing.Circle.CenterCoordinates.X},{myRing.Circle.CenterCoordinates.Y})");
            Console.WriteLine($"Внешний радиус: {myRing.Radius}");
            Console.WriteLine($"Внутренний радиус: {myRing.Circle.Radius}");
            Console.WriteLine($"Площадь кольца: {myRing.Area}");
            Console.WriteLine($"Суммарная длина окружностей: {myRing.TotalCircumference}");
            Console.WriteLine();
        }
    }
}
