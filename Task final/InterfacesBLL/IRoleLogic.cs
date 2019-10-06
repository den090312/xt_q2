using Entities;
using System.Collections.Generic;

namespace InterfacesBLL
{
    public interface IRoleLogic
    {
        bool Add(ref Role role);

        bool Remove(int roleId);

        IEnumerable<Role> GetAll();

        bool ChangeName(ref Role role, string newName);

        bool NoRoles();

        Role GetById(int id);

        int GetIdByName(string name);
    }
}