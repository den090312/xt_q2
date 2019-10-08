using Entities;
using System.Collections.Generic;

namespace InterfacesBLL
{
    public interface IRoleLogic
    {
        bool Add(ref Role role);

        bool Remove(int id);

        IEnumerable<Role> GetAll();

        bool ChangeName(ref Role role, string newName);

        bool NoRoles();

        Role Get(int id);

        int GetId(string name);
    }
}