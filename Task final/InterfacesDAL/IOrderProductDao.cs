using Entities;
using System.Collections.Generic;

namespace InterfacesDAL
{
    public interface IOrderProductDao
    {
        bool Add(OrderProduct orderProduct);

        IEnumerable<int> GetProductIds(int orderId);
    }
}