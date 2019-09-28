using System;
using System.Collections.Generic;

namespace Entities
{
    public class Customer
    {
        public string Name { get; }

        public User User { get; }

        public List<Order> Orders { get; set; }

        public Customer(User user, string name)
        {
            Name = name;
            User = user;
        }
    }
}
