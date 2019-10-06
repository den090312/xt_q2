using Entities;
using System.Collections.Generic;

namespace InterfacesBLL
{
    public interface IOrderProductLogic
    {
        bool Add(OrderProduct orderProduct);

        IEnumerable<int> GetProductIds(int orderId);

        bool RemoveByOrderId(int orderId);

        bool RemoveByProductId(int productId);

        IEnumerable<OrderProduct> GetByOrderId(int orderId);

        IEnumerable<OrderProduct> GetByProductId(int productId);

        IEnumerable<OrderProduct> GetAll();

        bool NoOrderProducts();
    }
}
