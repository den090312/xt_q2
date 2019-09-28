using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Nomenclature
    {
        public string Name { get; }

        public Double Price { get; }

        public Nomenclature(string name, double price)
        {
            NullCheck(name);
            EmptyStringCheck(name);
            PriceCheck(price);

            Name = name;
            Price = price;
        }

        private void EmptyStringCheck(string inputString)
        {
            if (inputString == string.Empty)
            {
                throw new ArgumentException($"{nameof(inputString)} is empty!");
            }
        }

        private void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }

        private void PriceCheck(double price)
        {
            if (price <= 0)
            {
                throw new ArgumentException($"{nameof(price)} must be greater than zero!");
            }
        }
    }
}
