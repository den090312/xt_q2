using Entities;
using System.Collections.Generic;

namespace InterfacesDAL
{
    public interface IProductDao
    {
        bool Add(ref Product product);

        bool NoProducts();

        IEnumerable<Product> GetAll();

        Product Get(int id);

        bool Remove(int id);
    }
}