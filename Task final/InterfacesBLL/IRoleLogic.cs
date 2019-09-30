using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesBLL
{
    public interface IRoleLogic
    {
        bool Add(ref Role role);

        bool Remove(int roleId);

        IEnumerable<Role> GetAll();

        bool ChangeName(ref Role role, string newName);

        bool NoRoles();
    }
}
