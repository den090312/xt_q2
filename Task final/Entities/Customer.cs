using System;
using System.Collections.Generic;

namespace Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public int IdUser { get; }

        public string Name { get; }

        public List<int> ListOrderId { get; set; }

        public Customer(string name, User user)
        {
            IdUser = user.Id;
            Name = name;
        }

        public Customer(int id, int idUser, string name)
        {
            Id = id;
            IdUser = idUser;
            Name = name;
        }
    }
}