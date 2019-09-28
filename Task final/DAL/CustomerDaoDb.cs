using Entities;
using InterfacesDAL;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class CustomerDaoDb : ICustomerDao
    {
        public bool Add(Customer customer)
        {
            throw new NotImplementedException();
        }

        public bool AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public bool ChangeName(string newName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrders()
        {
            throw new NotImplementedException();
        }

        public bool Remove(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void RemoveOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
