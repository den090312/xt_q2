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

        IEnumerable<Order> GetByIdCustomer(int id);

        IEnumerable<Order> GetByIdManager(int id);

        bool CancelOrder(int id);

        bool RestoreOrder(int id);

        bool CompleteOrder(int id);

        IEnumerable<Order> GetNewOrders();

        bool InWorkOrder(int orderId, int idManager);
    }
}
