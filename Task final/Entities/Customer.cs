using System;
using System.Collections.Generic;

namespace Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public int IdUser { get; }

        public string Name { get; }

        public List<int> ListOrderId { get; set; }

        public Customer(string name, int idUser)
        {
            IdUser = idUser;
            Name = name;
        }
    }
}