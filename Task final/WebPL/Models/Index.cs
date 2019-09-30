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

        public static void LoadDemoData()
        {
            LoadDemoRoles(out Role roleGuest, out Role roleCustomer, out Role roleManager, out Role roleAdmin, out Role roleSuperAdmin);
            LoadDemoUsers(roleGuest, roleCustomer, roleManager, roleAdmin, roleSuperAdmin);
        }

        private static void LoadDemoUsers(Role roleGuest, Role roleCustomer, Role roleManager, Role roleAdmin, Role roleSuperAdmin)
        {
            var userGuest = User.Guest;
            Dependencies.UserLogic.Add(ref userGuest, roleGuest.Id);

            var userCustomer = User.Customer;
            Dependencies.UserLogic.Add(ref userCustomer, roleCustomer.Id);

            var userManager = User.Manager;
            Dependencies.UserLogic.Add(ref userManager, roleManager.Id);

            var userAdmin = User.Manager;
            Dependencies.UserLogic.Add(ref userAdmin, roleAdmin.Id);

            var userSuperAdmin = User.Manager;
            Dependencies.UserLogic.Add(ref userSuperAdmin, roleSuperAdmin.Id);
        }

        private static void LoadDemoRoles(out Role roleGuestId, out Role roleCustomer, out Role roleManager, out Role roleAdmin, out Role roleSuperAdmin)
        {
            roleGuestId = Role.Guest;
            Dependencies.RoleLogic.Add(ref roleGuestId);

            roleCustomer = Role.Customer;
            Dependencies.RoleLogic.Add(ref roleCustomer);

            roleManager = Role.Manager;
            Dependencies.RoleLogic.Add(ref roleManager);

            roleAdmin = Role.Admin;
            Dependencies.RoleLogic.Add(ref roleAdmin);

            roleSuperAdmin = Role.SuperAdmin;
            Dependencies.RoleLogic.Add(ref roleSuperAdmin);
        }
    }
}