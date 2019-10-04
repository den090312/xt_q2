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
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderDao orderDao;

        public OrderLogic(IOrderDao iOrderDao)
        {
            NullCheck(iOrderDao);

            orderDao = iOrderDao;
        }

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

        private void DateCheck(DateTime date)
        {
            if (date == DateTime.MinValue)
            {
                throw new ArgumentException($"{nameof(date)} is empty!");
            }

            if (date < DateTime.Now)
            {
                throw new ArgumentException($"{nameof(date)} must be grater than current date!");
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
