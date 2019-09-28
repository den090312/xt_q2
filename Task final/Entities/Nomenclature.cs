using System;

namespace Entities
{
    public class Nomenclature
    {
        public int NomenclatureId { get; set; }

        public string Name { get; }

        public Double Price { get; }

        public Nomenclature(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
}
