using BLL;
using InterfacesBLL;
using InterfacesDAL;
using DAL;
using System.Configuration;

namespace Resolver
{
    public static class Resolver
    {
        private static readonly IUserDao userDao;
        private static readonly ICustomerDao customerDao;

        public static IUserLogic UserLogic { get; private set; }

        public static ICustomerLogic CustomerLogic { get; private set; }

        static Resolver()
        {
            var userDaoSet = ConfigurationManager.AppSettings["userDaoSet"];

            switch (userDaoSet)
            {
                case "1":
                    userDao = new UserDaoDb();
                    break;
                default:
                    throw new ConfigurationErrorsException($"Can't find settings for {nameof(userDaoSet)}!");
            }

            var customerDaoSet = ConfigurationManager.AppSettings["customerDaoSet"];

            switch (customerDaoSet)
            {
                case "1":
                    customerDao = new CustomerDaoDb();
                    break;
                default:
                    throw new ConfigurationErrorsException($"Can't find settings for {nameof(customerDaoSet)}!");
            }

            CustomerLogic = new CustomerLogic(customerDao);
            UserLogic = new UserLogic(userDao);
        }
    }
}
