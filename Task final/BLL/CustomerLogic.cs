using Entities;
using InterfacesBLL;
using InterfacesDAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class CustomerLogic : ICustomerLogic
    {
        private readonly ICustomerDao customerDao;

        public CustomerLogic(ICustomerDao iCustomerDao)
        {
            NullCheck(iCustomerDao);

            customerDao = iCustomerDao;
        }

        public bool Add(Customer customer)
        {
            NullCheck(customer); 

            return customerDao.Add(customer);
        }

        public bool Remove(Customer customer)
        {
            NullCheck(customer);

            return customerDao.Remove(customer);
        }

        public IEnumerable<Customer> GetAll() => customerDao.GetAll();

        public bool AddOrder(Order order)
        {
            NullCheck(order);

            return customerDao.AddOrder(order);
        }

        public void RemoveOrder(Order order)
        {
            NullCheck(order);

            customerDao.RemoveOrder(order);
        }

        public IEnumerable<Order> GetOrders()
        {
            return customerDao.GetOrders();
        }

        public bool ChangeName(string newName)
        {
            NullCheck(newName);

            return customerDao.ChangeName(newName);
        }

        public bool ConnectToUser(User user)
        {
            NullCheck(user);

            return customerDao.ConnectToUser(user);
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

        private void PriceCheck(double price)
        {
            if (price <= 0)
            {
                throw new ArgumentException($"{nameof(price)} must be greater than zero!");
            }
        }
    }
}
