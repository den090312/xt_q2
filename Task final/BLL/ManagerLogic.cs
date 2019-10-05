using Entities;
using InterfacesBLL;
using InterfacesDAL;
using log4net;
using System;

namespace BLL
{
    public class ManagerLogic : IManagerLogic, ILoggerLogic
    {
        private readonly IManagerDao managerDao;

        private readonly ILoggerDao loggerDao;

        public ILog Log => loggerDao.Log;

        public ManagerLogic(IManagerDao iManagerDao, ILoggerDao iLoggerDao)
        {
            NullCheck(iManagerDao);
            NullCheck(iLoggerDao);

            managerDao = iManagerDao;
            loggerDao = iLoggerDao;
        }

        public void InitLogger() => loggerDao.StartLogger();

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
