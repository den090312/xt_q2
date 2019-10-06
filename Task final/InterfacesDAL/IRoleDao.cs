using Entities;
using System.Collections.Generic;

namespace InterfacesDAL
{
    public interface IRoleDao
    {
        bool Add(ref Role role);

        bool Remove(int roleId);

        IEnumerable<Role> GetAll();

        bool UpdateName(ref Role role);

        bool NoRoles();

        Role GetById(int id);

        int GetIdByName(string name);
    }
}