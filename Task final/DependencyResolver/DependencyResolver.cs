using BLL;
using InterfacesBLL;
using InterfacesDAL;
using DAL;
using System.Configuration;

namespace DependencyResolver
{
    public static class DependencyResolver
    {
        private static readonly ICustomerDao customerDao;

        public static ICustomerLogic CustomerLogic { get; private set; }

        static DependencyResolver()
        {
            var customDaoSet = ConfigurationManager.AppSettings["customDaoSet"];

            if (customDaoSet == "1")
            {
                customerDao = new CustomerDaoDb();
            }
            else
            {
                throw new ConfigurationErrorsException($"Can't find settings for {nameof(customDaoSet)}!");
            }

            CustomerLogic = new CustomerLogic(customerDao);
        }
    }
}
