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

        public IEnumerable<Order> GetByCustomerId(int id)
        {
            IdCheck(id);

            return orderDao.GetByIdCustomer(id);
        }

        public IEnumerable<Order> GetByManagerId(int id)
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
