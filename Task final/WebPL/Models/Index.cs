using Entities;
using Common;
using System.Collections.Specialized;
using System;

namespace WebPL.Models
{
    public class Index
    {
        public static NameValueCollection Forms { private get; set; }

        public static string Message { get; set; }

        public static User CurrentUser { get; set; }

        static Index() => CurrentUser = User.Guest;

        public static void Run()
        {
            Account();
            LoadDemoData();
            Order();
        }

        private static void Order()
        {
            var productId = Forms["chosenProductId"];
            var quantity = Forms["chosenProductQuantity"];

            if (productId == null || quantity == null || productId == string.Empty || quantity == string.Empty)
            {
                return;
            }


        }

        private static void Account()
        {
            TryLogIn();
            LogOut();
            Register();
        }

        private static void Register()
        {
            var regName  = Forms["regName"];
            var regPass  = Forms["regPass"];
            var regRole  = Forms["regRole"];

            if (regName == null || regPass == null || regName == string.Empty || regPass == string.Empty)
            {
                return;
            }

            if (CurrentUser == User.Guest)
            {
                RegisterUser(regName, regPass, Role.Customer);
            }
        }

        private static void RegisterUser(string regName, string regPass, Role regRole)
        {
            var userLogic = Dependencies.UserLogic;

            if (userLogic.GetByName(regName) != null)
            {
                Message = "User is already exists!";

                return;
            }

            var roleId = Dependencies.RoleLogic.GetIdByName(regRole.Name);

            if (userLogic.Add(roleId, regName, regPass))
            {
                CurrentUser = Dependencies.UserLogic.GetByName(regName);
                Message = "User registered";

                return;
            }
            else
            {
                Message = "User was NOT registered!";

                return;
            }
        }

        private static void LogOut()
        {
            var loggedOut = Forms["loggedOut"];

            if (loggedOut != null & loggedOut == "loggedOut")
            {
                CurrentUser = User.Guest;
                Message = string.Empty;
            }
        }

        private static void TryLogIn()
        {
            var logName = Forms["logName"];
            var logPass = Forms["logPass"];

            if (logName == null || logPass == null)
            {
                return;
            }

            var userLogic = Dependencies.UserLogic;

            var logUser = userLogic.GetByName(logName);

            if (logUser == null)
            {
                Message = "User name is not exists!";

                return;
            }

            if (!userLogic.PasswordIsOk(logPass, logUser.PasswordHash))
            {
                Message = "Wrong password!";

                return;
            }
            else
            {
                CurrentUser = logUser;
                Message = string.Empty;
            }
        }

        private static void LoadDemoData()
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

        public static string GetLastError()
        {
            return string.Empty;
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

        private static void LoadDemoUsers(Role roleGuest, Role roleCustomer, Role roleManager, Role roleAdmin, Role roleSuperAdmin)
        {
            var userLogic = Dependencies.UserLogic;

            var userGuest = User.Guest;
            userLogic.Add(ref userGuest, roleGuest.Id, "Guest");

            var userCustomer = User.Customer;
            userLogic.Add(ref userCustomer, roleCustomer.Id, "Customer");

            var userManager = User.Manager;
            userLogic.Add(ref userManager, roleManager.Id, "Manager");

            var userAdmin = User.Admin;
            userLogic.Add(ref userAdmin, roleAdmin.Id, "Admin");

            var userSuperAdmin = User.SuperAdmin;
            userLogic.Add(ref userSuperAdmin, roleSuperAdmin.Id, "SuperAdmin");
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