using Entities;
using InterfacesDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrderProductDaoDb : IOrderProductDao
    {
        public bool Add(OrderProduct orderProduct)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderProduct> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderProduct> GetByOrderId(int orderId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderProduct> GetByProductId(int productId)
        {
            throw new NotImplementedException();
        }

        public bool NoOrderProducts()
        {
            throw new NotImplementedException();
        }

        public bool RemoveByOrderId(int orderId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveByProductId(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
