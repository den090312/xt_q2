using Entities;
using InterfacesBLL;
using InterfacesDAL;
using log4net;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class OrderLogic : IOrderLogic, ILoggerLogic
    {
        private readonly IOrderDao orderDao;

        private readonly ILoggerDao loggerDao;

        public ILog Log => loggerDao.Log;

        public OrderLogic(IOrderDao iOrderDao)
        {
            NullCheck(iOrderDao);

            orderDao = iOrderDao;
        }

        public void InitLogger() => loggerDao.InitLogger();

        public bool Add(ref Order order)
        {
            NullCheck(order);
            IdCheck(order.IdCustomer);

            NullCheck(order.Adress);
            EmptyStringCheck(order.Adress);

            SumCheck(order.Sum);

            return orderDao.Add(ref order);
        }

        public bool CancelOrder(int id)
        {
            IdCheck(id);

            return orderDao.CancelOrder(id);
        }

        public bool RestoreOrder(int id)
        {
            IdCheck(id);

            return orderDao.RestoreOrder(id);
        }

        public bool InWorkOrder(int orderId, int idManager)
        {
            IdCheck(orderId);
            IdCheck(idManager);

            return orderDao.InWorkOrder(orderId, idManager);
        }

        public bool CompleteOrder(int id)
        {
            IdCheck(id);

            return orderDao.CompleteOrder(id);
        }

        public IEnumerable<Order> GetNewOrders()
        {
            return orderDao.GetNewOrders();
        }

        public IEnumerable<Order> GetByIdCustomer(int id)
        {
            IdCheck(id);

            return orderDao.GetByIdCustomer(id);
        }

        public IEnumerable<Order> GetByIdManager(int id)
        {
            IdCheck(id);

            return orderDao.GetByIdManager(id);
        }

        private void IdCheck(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException($"{nameof(id)} is incorrect!");
            }
        }

        private void SumCheck(decimal sum)
        {
            if (sum <= 0)
            {
                throw new ArgumentException($"{nameof(sum)} is incorrect!");
            }
        }

        private void EmptyStringCheck(string inputString)
        {
            if (inputString == string.Empty)
            {
                throw new ArgumentException($"{nameof(inputString)} is empty!");
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
