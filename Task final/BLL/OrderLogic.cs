using Entities;
using InterfacesBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OrderLogic : IOrderLogic
    {
        public IEnumerable<Order> GetByCustomerId(int customerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetByManagerId(int managerId)
        {
            throw new NotImplementedException();
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
    }
}
