﻿using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesDAL
{
    public interface IOrderProductDao
    {
        bool Add(OrderProduct orderProduct);

        bool RemoveByOrderId(int orderId);

        bool RemoveByProductId(int productId);

        IEnumerable<OrderProduct> GetByOrderId(int orderId);

        IEnumerable<OrderProduct> GetByProductId(int productId);

        IEnumerable<OrderProduct> GetAll();

        bool NoOrderProducts();
    }
}
