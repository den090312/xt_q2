using Entities;
using System.Collections.Generic;

namespace InterfacesDAL
{
    public interface IProductDao
    {
        bool Add(ref Product product);

        bool NoProducts();

        IEnumerable<Product> GetAll();

        Product GetById(int id);

        bool Remove(int id);
    }
}