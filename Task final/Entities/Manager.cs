using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Manager
    {
        public string Name { get; }

        public User User { get; }

        public List<Order> Orders { get; set; }

        public enum Rank
        {
            None = 0,
            Junior = 1,
            Middle = 2,
            Top = 3,
            General = 4
        }

        public Manager(string name, User user)
        {
            Name = name;
            User = user;
        }
    }
}
