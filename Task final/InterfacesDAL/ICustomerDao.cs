using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesDAL
{
    public interface ICustomerDao
    {
        bool Add(Customer customer);

        bool Remove(Customer customer);

        IEnumerable<Customer> GetAll();

        bool AddOrder(Order order);

        void RemoveOrder(Order order);

        IEnumerable<Order> GetOrders();

        bool ChangeName(string newName);
    }
}
