using Entities;
using InterfacesBLL;
using InterfacesDAL;
using log4net;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class RoleLogic : IRoleLogic, ILoggerLogic
    {
        private readonly IRoleDao roleDao;

        private readonly ILoggerDao loggerDao;

        public ILog Log => loggerDao.Log;

        public RoleLogic(IRoleDao iRoleDao, ILoggerDao iLoggerDao)
        {
            NullCheck(iRoleDao);
            NullCheck(iLoggerDao);

            roleDao = iRoleDao;
            loggerDao = iLoggerDao;
        }

        public void InitLogger() => loggerDao.StartLogger();

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

        public bool NoRoles() => roleDao.NoRoles();

        public Role GetById(int id)
        {
            IdCheck(id);

            return roleDao.GetById(id);
        }

        public int GetIdByName(string name)
        {
            NullCheck(name);
            EmptyStringCheck(name);

            return roleDao.GetIdByName(name);
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
