using Entities;
using InterfacesDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RoleDaoDb : IRoleDao
    {
        public bool Add(Role role)
        {
            throw new NotImplementedException();
        }

        public bool AddFullPermissons(Role role)
        {
            throw new NotImplementedException();
        }

        public bool AddReadonly(Role role)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Remove(int RoleId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateName(Role role)
        {
            throw new NotImplementedException();
        }
    }
}