using System.Collections.Generic;
using Entities;

namespace InterfacesDAL
{
    public interface IOrderDao
    {
        bool Add(ref Order order);

        IEnumerable<Order> GetByIdCustomer(int id);

        IEnumerable<Order> GetByIdManager(int id);

        bool CancelOrder(int id);

        bool RestoreOrder(int id);

        bool InWorkOrder(int orderId, int idManager);

        IEnumerable<Order> GetNewOrders();

        bool CompleteOrder(int id);
    }
}