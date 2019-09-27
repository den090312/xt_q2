using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        bool ConnectToUser(User user);

        bool ChangeName(string newName);
    }
}
