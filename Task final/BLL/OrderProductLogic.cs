using Entities;
using InterfacesBLL;
using InterfacesDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OrderProductLogic : IOrderProductLogic
    {
        private readonly IOrderProductDao orderProductDao;

        public OrderProductLogic(IOrderProductDao iOrderProductDao)
        {
            NullCheck(iOrderProductDao);

            orderProductDao = iOrderProductDao;
        }

        public bool Add(OrderProduct orderProduct)
        {
            NullCheck(orderProduct);

            IdCheck(orderProduct.IdOrder);
            IdCheck(orderProduct.IdProduct);

            return orderProductDao.Add(orderProduct);
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

        private void IdCheck(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException($"{nameof(id)} is incorrect!");
            }
        }

        private void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
    }
}
