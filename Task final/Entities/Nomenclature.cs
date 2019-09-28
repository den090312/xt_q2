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
            Name = name;
            Price = price;
        }
    }
}
