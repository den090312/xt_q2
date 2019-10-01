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
        bool Add(int roleId, string name, string password);

        bool Add(ref User user, int roleId, string password);

        bool Remove(int userId);

        IEnumerable<User> GetAll();

        bool ChangeName(User user, string newName);
    }
}
