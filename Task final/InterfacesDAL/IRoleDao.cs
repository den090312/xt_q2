using Entities;
using System.Collections.Generic;

namespace InterfacesDAL
{
    public interface IRoleDao
    {
        bool Add(ref Role role);

        bool Remove(int id);

        IEnumerable<Role> GetAll();

        bool UpdateName(ref Role role);

        bool NoRoles();

        Role Get(int id);

        int GetId(string name);
    }
}