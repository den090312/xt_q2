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
            LoadDefaultRoles(out Role roleGuest, out Role roleCustomer, out Role roleManager, out Role roleAdmin, out Role roleSuperAdmin);

            LoadDefaultUsers(roleGuest, roleCustomer, roleManager, roleAdmin, roleSuperAdmin, 
                out User userGuest, out User userCustomer, out User userManager, out User userAdmin, out User userSuperAdmin);


        }

        private static void LoadDefaultUsers(Role roleGuest, Role roleCustomer, Role roleManager, Role roleAdmin, Role roleSuperAdmin,
            out User userGuest, out User userCustomer, out User userManager, out User userAdmin, out User userSuperAdmin)
        {
            userGuest = User.Guest;
            Dependencies.UserLogic.Add(ref userGuest, roleGuest.Id);

            userCustomer = User.Customer;
            Dependencies.UserLogic.Add(ref userCustomer, roleCustomer.Id);

            userManager = User.Manager;
            Dependencies.UserLogic.Add(ref userCustomer, roleManager.Id);

            userAdmin = User.Manager;
            Dependencies.UserLogic.Add(ref userAdmin, roleAdmin.Id);

            userSuperAdmin = User.Manager;
            Dependencies.UserLogic.Add(ref userSuperAdmin, roleSuperAdmin.Id);
        }

        private static void LoadDefaultRoles(out Role roleGuestId, out Role roleCustomer, out Role roleManager, out Role roleAdmin, out Role roleSuperAdmin)
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