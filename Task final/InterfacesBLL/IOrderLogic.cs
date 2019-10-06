using Entities;
using System.Collections.Generic;

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