using Entities;
using InterfacesBLL;
using InterfacesDAL;
using log4net;
using System;

namespace BLL
{
    public class CustomerLogic : ICustomerLogic, ILoggerLogic
    {
        private readonly ICustomerDao customerDao;

        private readonly ILoggerDao loggerDao;

        public ILog Log => loggerDao.Log;

        public CustomerLogic(ICustomerDao iCustomerDao, ILoggerDao iLoggerDao)
        {
            NullCheck(iCustomerDao);
            NullCheck(iLoggerDao);

            customerDao = iCustomerDao;
            loggerDao = iLoggerDao;
        }

        public void StartLogger() => loggerDao.StartLogger();

        public bool Add(ref Customer customer)
        {
            NullCheck(customer);

            return customerDao.Add(ref customer);
        }

        public Customer GetByUserId(int userId)
        {
            IdCheck(userId);

            return customerDao.GetByIdUser(userId);
        }

        private void IdCheck(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException($"{nameof(id)} is incorrect!");
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
