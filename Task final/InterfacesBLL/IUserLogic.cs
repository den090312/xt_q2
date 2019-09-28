using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesBLL
{
    public interface IUserLogic
    {
        bool Add(User user);

        bool Remove(int userId);

        IEnumerable<User> GetAll();

        bool ChangeName(User user, string newName);
    }
}
