﻿using BLL;
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
        private static readonly IOrderDao orderDao;
        private static readonly IOrderProductDao orderProductDao;
        private static readonly IManagerDao managerDao;
        private static readonly ILoggerDao loggerDao;

        public static IRoleLogic RoleLogic { get; private set; }

        public static IUserLogic UserLogic { get; private set; }

        public static ICustomerLogic CustomerLogic { get; private set; }

        public static IProductLogic ProductLogic { get; private set; }

        public static IOrderLogic OrderLogic { get; private set; }

        public static IOrderProductLogic OrderProductLogic { get; private set; }

        public static IManagerLogic ManagerLogic { get; private set; }

        public static ILoggerLogic LoggerLogic { get; private set; }

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

            var orderDaoSet = ConfigurationManager.AppSettings["orderDaoSet"];

            switch (orderDaoSet)
            {
                case "1":
                    orderDao = new OrderDaoDb();
                    break;
                default:
                    throw new ConfigurationErrorsException($"Can't find settings for {nameof(orderDaoSet)}!");
            }

            var orderProductDaoSet = ConfigurationManager.AppSettings["orderProductDaoSet"];

            switch (orderProductDaoSet)
            {
                case "1":
                    orderProductDao = new OrderProductDaoDb();
                    break;
                default:
                    throw new ConfigurationErrorsException($"Can't find settings for {nameof(orderProductDaoSet)}!");
            }

            var managerDaoSet = ConfigurationManager.AppSettings["managerDaoSet"];

            switch (managerDaoSet)
            {
                case "1":
                    managerDao = new ManagerDaoDb();
                    break;
                default:
                    throw new ConfigurationErrorsException($"Can't find settings for {nameof(managerDaoSet)}!");
            }

            var loggerDaoSet = ConfigurationManager.AppSettings["loggerDaoSet"];

            switch (loggerDaoSet)
            {
                case "2":
                    loggerDao = new LoggerDaoFile();
                    break;
                default:
                    throw new ConfigurationErrorsException($"Can't find settings for {nameof(loggerDaoSet)}!");
            }

            RoleLogic         = new RoleLogic(roleDao);
            UserLogic         = new UserLogic(userDao);
            CustomerLogic     = new CustomerLogic(customerDao);
            ProductLogic      = new ProductLogic(productDao);
            OrderLogic        = new OrderLogic(orderDao);
            OrderProductLogic = new OrderProductLogic(orderProductDao);
            ManagerLogic      = new ManagerLogic(managerDao);
            LoggerLogic       = new LoggerLogic(loggerDao); 
        }
    }
}
