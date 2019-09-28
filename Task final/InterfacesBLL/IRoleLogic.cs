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
        bool Add(string name);

        bool AddReadonly(string name);

        bool AddFullPermissons(string name);

        bool Remove(int RoleId);

        IEnumerable<Role> GetAll();

        bool ChangeName(Role role, string newName);
    }
}
