using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesDAL
{
    public interface ICustomerDao
    {
        bool Add(ref Customer customer);

        Customer GetByUserId(int userId);

        bool Remove(Customer customer);

        IEnumerable<Customer> GetAll();

        bool AddOrder(Order order);

        void RemoveOrder(Order order);

        IEnumerable<Order> GetOrders();

        bool ChangeName(string newName);
    }
}
