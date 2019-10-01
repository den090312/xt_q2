using Entities;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPL.Models
{
    public class Index
    {
        public static User CurrentUser { get; set; }

        static Index() => CurrentUser = User.Guest;

        public static void DemoData()
        {
            if (Dependencies.RoleLogic.NoRoles())
            {
                LoadDemoRoles(out Role roleGuest, out Role roleCustomer, out Role roleManager, out Role roleAdmin, out Role roleSuperAdmin);
                LoadDemoUsers(roleGuest, roleCustomer, roleManager, roleAdmin, roleSuperAdmin);
            }

            if (Dependencies.ProductLogic.NoProducts())
            {
                LoadDemoProducts();
            }
        }

        private static void LoadDemoRoles(out Role roleGuestId, out Role roleCustomer, out Role roleManager, out Role roleAdmin, out Role roleSuperAdmin)
        {
            var roleLogic = Dependencies.RoleLogic;

            roleGuestId = Role.Guest;
            roleLogic.Add(ref roleGuestId);

            roleCustomer = Role.Customer;
            roleLogic.Add(ref roleCustomer);

            roleManager = Role.Manager;
            roleLogic.Add(ref roleManager);

            roleAdmin = Role.Admin;
            roleLogic.Add(ref roleAdmin);

            roleSuperAdmin = Role.SuperAdmin;
            roleLogic.Add(ref roleSuperAdmin);
        }

        private static void LoadDemoUsers(Role roleGuest, Role roleCustomer, Role roleManager, Role roleAdmin, Role roleSuperAdmin)
        {
            var userLogic = Dependencies.UserLogic;

            var userGuest = User.Guest;
            userLogic.Add(ref userGuest, roleGuest.Id);

            var userCustomer = User.Customer;
            userLogic.Add(ref userCustomer, roleCustomer.Id);

            var userManager = User.Manager;
            userLogic.Add(ref userManager, roleManager.Id);

            var userAdmin = User.Manager;
            userLogic.Add(ref userAdmin, roleAdmin.Id);

            var userSuperAdmin = User.Manager;
            userLogic.Add(ref userSuperAdmin, roleSuperAdmin.Id);
        }

        private static void LoadDemoProducts()
        {
            var productLogic = Dependencies.ProductLogic;

            var drill = new Product("Дрель", 7500);
            productLogic.Add(ref drill);

            var speakers = new Product("Колонки", 3500);
            productLogic.Add(ref speakers);

            var fireworks = new Product("Хлопушка", 1000);
            productLogic.Add(ref fireworks);
        }
    }
}