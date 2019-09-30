using BLL;
using InterfacesBLL;
using InterfacesDAL;
using DAL;
using System.Configuration;

namespace Common
{
    public static class Dependencies
    {
        private static readonly IRoleDao roleDao;
        private static readonly IUserDao userDao;
        private static readonly ICustomerDao customerDao;
        private static readonly IProductDao productDao;

        public static IRoleLogic RoleLogic { get; private set; }

        public static IUserLogic UserLogic { get; private set; }

        public static ICustomerLogic CustomerLogic { get; private set; }

        public static IProductLogic ProductLogic { get; private set; }

        static Dependencies()
        {
            var roleDaoSet = ConfigurationManager.AppSettings["roleDaoSet"];

            switch (roleDaoSet)
            {
                case "1":
                    roleDao = new RoleDaoDb();
                    break;
                default:
                    throw new ConfigurationErrorsException($"Can't find settings for {nameof(roleDaoSet)}!");
            }

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
                    productDao = new ProductDaoDb();
                    break;
                default:
                    throw new ConfigurationErrorsException($"Can't find settings for {nameof(customerDaoSet)}!");
            }

            var productDaoSet = ConfigurationManager.AppSettings["productDaoSet"];

            switch (productDaoSet)
            {
                case "1":
                    customerDao = new CustomerDaoDb();
                    break;
                default:
                    throw new ConfigurationErrorsException($"Can't find settings for {nameof(productDaoSet)}!");
            }

            RoleLogic     = new RoleLogic(roleDao);
            UserLogic     = new UserLogic(userDao);
            CustomerLogic = new CustomerLogic(customerDao);
            ProductLogic  = new ProductLogic(productDao);
        }
    }
}
