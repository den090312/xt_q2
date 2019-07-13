using _2._1.ROUND;
using System;

namespace _2._6.RING
{
    public class Ring
    {
        private Radius outerRadius;
        private Radius innerRadius;

        public Point СenterCoordinates = new Point(0, 0);

        public Radius InnerRadius
        {
            get => innerRadius;
            set
            {
                RadiusCheck(innerRadius, outerRadius);
                innerRadius = value;
            }
        }
        public Radius OuterRadius
        {
            get => outerRadius;
            set
            {
                RadiusCheck(innerRadius, outerRadius);
                outerRadius = value;
            }
        }

        public double Area => Math.PI * (InnerRadius.Value * InnerRadius.Value - OuterRadius.Value * OuterRadius.Value);
        public double TotalCircumference => 2 * Math.PI * InnerRadius.Value + 2 * Math.PI * OuterRadius.Value;

        public Ring(Point userCenterCoordinates, Radius userInnerRadius, Radius userOuterRadius)
        {
            СenterCoordinates = userCenterCoordinates;
            InnerRadius = userInnerRadius;
            OuterRadius = userOuterRadius;
        }

        private void RadiusCheck(Radius innerRadius, Radius outerRadius)
        {
            if (outerRadius.Value < innerRadius.Value)
            {
                throw new ArgumentException("Внешний радиус не может быть меньше внутреннего!");
            }

            if (innerRadius.Value == outerRadius.Value)
            {
                throw new ArgumentException("Внешний радиус не может быть равен внутреннему!");
            }
        }
    }
}
