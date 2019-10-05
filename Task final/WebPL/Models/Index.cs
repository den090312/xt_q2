using Entities;
using Common;
using System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebPL.Models
{
    public class Index
    {
        public static NameValueCollection Forms { private get; set; }

        public static string Message { get; set; }

        public static User CurrentUser { get; set; }

        static Index()
        {
            Message = string.Empty;
            CurrentUser = Models.Account.CurrentUser;
        }

        public static void Run()
        {
            Account();
            Order();
            Product();
        }

        public static void DemoData()
        {
            if (Dependencies.RoleLogic.NoRoles())
            {
                LoadDemoRoles(out Role roleGuest, out Role roleCustomer, out Role roleManager, out Role roleAdmin, out Role roleSuperAdmin);
                LoadDemoUsers(roleGuest, roleCustomer, roleManager, roleAdmin, roleSuperAdmin, out int userManagerId);
                LoadGeneralManager(userManagerId);
            }

            if (Dependencies.ProductLogic.NoProducts())
            {
                LoadDemoProducts();
            }
        }

        private static void LoadGeneralManager(int userManagerId)
        {
            var manager = new Manager(userManagerId, "Manager", Manager.Rank.General);

            Dependencies.ManagerLogic.Add(ref manager);
        }

        public static string GetLastError()
        {
            return string.Empty;
        }

        private static void Account()
        {
            Models.Account.Run(Forms);

            var message = Models.Account.Message;

            if (!string.IsNullOrEmpty(message))
            {
                switch (message)
                {
                    case "ok":
                        Message = string.Empty;
                        break;
                    default:
                        Message = message;
                        break;
                }
            }

            CurrentUser = Models.Account.CurrentUser;
        }

        private static void Order()
        {
            Models.Order.Run(Forms);

            var message = Models.Order.Message;

            if (!string.IsNullOrEmpty(message))
            {
                Message = message;
            }
        }

        private static void Product()
        {
            Models.Product.Run(Forms);

            var message = Models.Product.Message;

            if (!string.IsNullOrEmpty(message))
            {
                Message = message;
            }
        }

        private static void LoadDemoRoles(out Role roleGuest, out Role roleCustomer, out Role roleManager, out Role roleAdmin, out Role roleSuperAdmin)
        {
            var roleLogic = Dependencies.RoleLogic;

            roleGuest = Role.Guest;
            roleLogic.Add(ref roleGuest);

            roleCustomer = Role.Customer;
            roleLogic.Add(ref roleCustomer);

            roleManager = Role.Manager;
            roleLogic.Add(ref roleManager);

            roleAdmin = Role.Admin;
            roleLogic.Add(ref roleAdmin);

            roleSuperAdmin = Role.SuperAdmin;
            roleLogic.Add(ref roleSuperAdmin);
        }

        private static void LoadDemoUsers(Role roleGuest, Role roleCustomer, Role roleManager, Role roleAdmin, Role roleSuperAdmin, out int userManagerId)
        {
            var userLogic = Dependencies.UserLogic;

            var userGuest = User.Guest;
            userLogic.Add(ref userGuest, roleGuest.Id, "Guest");

            var userCustomer = User.Customer;
            userLogic.Add(ref userCustomer, roleCustomer.Id, "Customer");

            var userManager = User.Manager;
            userLogic.Add(ref userManager, roleManager.Id, "Manager");
            userManagerId = userManager.Id;

            var userAdmin = User.Admin;
            userLogic.Add(ref userAdmin, roleAdmin.Id, "Admin");

            var userSuperAdmin = User.SuperAdmin;
            userLogic.Add(ref userSuperAdmin, roleSuperAdmin.Id, "SuperAdmin");
        }

        private static void LoadDemoProducts()
        {
            var productLogic = Dependencies.ProductLogic;

            var drill = new Entities.Product("Дрель", 7500);
            productLogic.Add(ref drill);

            var speakers = new Entities.Product("Колонки", 3500);
            productLogic.Add(ref speakers);

            var fireworks = new Entities.Product("Хлопушка", 1000);
            productLogic.Add(ref fireworks);
        }
    }
}