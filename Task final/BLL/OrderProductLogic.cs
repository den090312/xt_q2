using Entities;
using InterfacesBLL;
using InterfacesDAL;
using log4net;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class OrderProductLogic : IOrderProductLogic, ILoggerLogic
    {
        private readonly IOrderProductDao orderProductDao;

        private readonly ILoggerDao loggerDao;

        public ILog Log => loggerDao.Log;

        public OrderProductLogic(IOrderProductDao iOrderProductDao, ILoggerDao iLoggerDao)
        {
            NullCheck(iOrderProductDao);
            NullCheck(iLoggerDao);

            orderProductDao = iOrderProductDao;
            loggerDao = iLoggerDao;
        }

        public void InitLogger() => loggerDao.StartLogger();

        public bool Add(OrderProduct orderProduct)
        {
            NullCheck(orderProduct);

            IdCheck(orderProduct.IdOrder);
            IdCheck(orderProduct.IdProduct);

            return orderProductDao.Add(orderProduct);
        }

        public IEnumerable<int> GetProductIds(int orderId)
        {
            IdCheck(orderId);

            return orderProductDao.GetProductIds(orderId);
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
