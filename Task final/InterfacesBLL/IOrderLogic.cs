using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesBLL
{
    public interface IOrderLogic
    {
        bool Add(ref Order order);

        IEnumerable<Order> GetByCustomerId(int id);

        IEnumerable<Order> GetByManagerId(int id);

        bool CancelOrder(int id);

        bool RestoreOrder(int id);
    }
}
