using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace InterfacesDAL
{
    public interface IOrderDao
    {
        bool Add(ref Order order);

        IEnumerable<Order> GetByIdCustomer(int id);

        bool CancelOrder(int id);

        bool RestoreOrder(int id);
    }
}
