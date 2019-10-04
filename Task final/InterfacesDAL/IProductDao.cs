using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
