using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesBLL
{
    public interface IProductLogic
    {
        bool Add(string name, decimal price);

        bool Add(ref Product product);

        bool NoProducts();

        IEnumerable<Product> GetAll();

        Product GetById(int id);
    }
}
