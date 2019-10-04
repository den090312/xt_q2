using Entities;
using InterfacesBLL;
using InterfacesDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ManagerLogic : IManagerLogic
    {
        private readonly IManagerDao managerDao;

        public ManagerLogic(IManagerDao iManagerDao)
        {
            NullCheck(iManagerDao);

            managerDao = iManagerDao;
        }

        public bool Add(ref Manager manager)
        {
            NullCheck(manager);
            IdCheck(manager.IdUser);

            NullCheck(manager.Name);
            EmptyStringCheck(manager.Name);

            return managerDao.Add(ref manager);
        }

        public Manager GetByUserId(int userId)
        {
            IdCheck(userId);

            return managerDao.GetByIdUser(userId);
        }

        public bool IsManager(int idUser)
        {
            IdCheck(idUser);

            return managerDao.IsManager(idUser);
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
