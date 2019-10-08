using Entities;
using System.Collections.Generic;

namespace InterfacesBLL
{
    public interface IProductLogic
    {
        bool Add(string name, decimal price);

        bool Add(ref Product product);

        bool NoProducts();

        IEnumerable<Product> GetAll();

        Product Get(int id);

        bool Remove(int id);
    }
}