using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesBLL
{
    public interface ICustomerLogic
    {
        bool Add(ref Customer customer);

        Customer GetByIdUser(int userId);

        bool Remove(Customer customer);

        IEnumerable<Customer> GetAll();

        bool AddOrder(Order order);

        void RemoveOrder(Order order);

        IEnumerable<Order> GetOrders();

        bool ChangeName(string newName);
    }
}
