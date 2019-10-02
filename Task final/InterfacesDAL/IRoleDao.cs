﻿using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesDAL
{
    public interface IRoleDao
    {
        bool Add(ref Role role);

        bool Remove(int roleId);

        IEnumerable<Role> GetAll();

        bool UpdateName(ref Role role);

        bool NoRoles();

        string GetNameById(int id);

        int GetIdByName(string name);
    }
}
