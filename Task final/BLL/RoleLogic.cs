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

        public bool Add(ref Role role)
        {
            NullCheck(role);
            EmptyStringCheck(role.Name);

            return roleDao.Add(ref role);
        }

        public bool ChangeName(ref Role role, string newName)
        {
            NullCheck(role);
            IdCheck(role.Id);

            NullCheck(newName);
            EmptyStringCheck(newName);

            role.Name = newName;

            return roleDao.UpdateName(ref role);
        }

        public IEnumerable<Role> GetAll() => roleDao.GetAll();

        public bool Remove(int RoleId)
        {
            IdCheck(RoleId);

            return roleDao.Remove(RoleId);
        }

        public bool NoRoles()
        {
            return roleDao.NoRoles();
        }

        public string GetNameById(int id)
        {
            IdCheck(id);

            return roleDao.GetNameById(id);
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
