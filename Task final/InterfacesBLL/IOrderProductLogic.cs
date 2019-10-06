using Entities;
using System.Collections.Generic;

namespace InterfacesBLL
{
    public interface IOrderProductLogic
    {
        bool Add(OrderProduct orderProduct);

        IEnumerable<int> GetProductIds(int orderId);
    }
}