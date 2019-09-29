using Entities;
using InterfacesBLL;
using InterfacesDAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class RoleLogic : IRoleLogic
    {
        private readonly IRoleDao roleDao;

        public RoleLogic(IRoleDao iRoleDao)
        {
            NullCheck(iRoleDao);

            roleDao = iRoleDao;
        }

        public bool Add(string name)
        {
            NullCheck(name);
            EmptyStringCheck(name);

            return roleDao.Add(new Role(name));
        }

        public bool AddReadonly(string name)
        {
            NullCheck(name);
            EmptyStringCheck(name);

            var role = new Role(name)
            {
                OrderRead = true,
                ProductRead = true,
                RoleRead = true,
                UserRead = true
            };

            return roleDao.Add(role);
        }

        public bool AddFullPermissons(string name)
        {
            NullCheck(name);
            EmptyStringCheck(name);

            var role = new Role(name)
            {
                OrderRead = true,
                OrderWrite = true,
                ProductRead = true,
                ProductWrite = true,
                RoleRead = true,
                RoleWrite = true,
                UserRead = true,
                UserWrite = true
            };

            return roleDao.Add(role);
        }

        public bool ChangeName(Role role, string newName)
        {
            NullCheck(role);
            IdCheck(role.Id);

            NullCheck(newName);
            EmptyStringCheck(newName);

            role.Name = newName;

            return roleDao.UpdateName(role);
        }

        public IEnumerable<Role> GetAll() => roleDao.GetAll();

        public bool Remove(int RoleId)
        {
            IdCheck(RoleId);

            return roleDao.Remove(RoleId);
        }

        private void IdCheck(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException($"{nameof(id)} is incorrect!");
            }
        }

        private void EmptyStringCheck(string inputString)
        {
            if (inputString == string.Empty)
            {
                throw new ArgumentException($"{nameof(inputString)} is empty!");
            }
        }

        private void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
    }
}
