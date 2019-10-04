﻿using Entities;
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

        static Index() => CurrentUser = User.Guest;

        public static void Run()
        {
            Account();
            Order();
        }

        private static void Account()
        {
            if (LogIn())
            {
                return;
            }

            if (LogOut())
            {
                return;
            }

            if (CurrentUser != User.Guest)
            {
                return;
            }

            var registerUser = RegisterUser();

            if (registerUser == User.Guest)
            {
                return;
            }

            RegisterCustomer(registerUser);
        }

        private static void Order()
        {
            if (AddOrder())
            {
                return;
            }

            if (CancelOrder())
            {
                return;
            }

            RestoreOrder();
        }

        public static IEnumerable<Order> GetOrderList()
        {
            var userType = Forms["userType"];
            var id = Forms["id"];

            if (userType == null || id == null || userType == string.Empty || id == string.Empty)
            {
                return Enumerable.Empty<Order>();
            }

            if (!int.TryParse(id, out int idParsed))
            {
                Message = $"Некорректный id - '{id}'!";

                return Enumerable.Empty<Order>();
            }

            if (userType == "customer")
            {
                Message = string.Empty;

                return Dependencies.OrderLogic.GetByCustomerId(idParsed);
            }

            return Enumerable.Empty<Order>();
        }

        private static bool CancelOrder()
        {
            var id = GetChosenOrderCancelId();

            if (id <= 0)
            {
                return false;
            }

            if (Dependencies.OrderLogic.CancelOrder(id))
            {
                Message = "Заказ отменен";

                return true;
            }
            else
            {
                Message = "Ошибка отмены заказа!";

                return false;
            }
        }


        private static void RestoreOrder()
        {
            var id = GetChosenOrderRestoreId();

            if (id <= 0)
            {
                return;
            }

            if (Dependencies.OrderLogic.RestoreOrder(id))
            {
                Message = "Заказ восстановлен";
            }
            else
            {
                Message = "Ошибка восстановления заказа!";
            }
        }

        private static int GetChosenOrderCancelId()
        {
            var id = Forms["chosenOrderCancelId"];

            if (id == null || id == string.Empty)
            {
                return 0;
            }

            if (!int.TryParse(id, out int idParsed))
            {
                Message = $"Некорректный id - {id}!";

                return 0;
            }

            return idParsed;
        }

        private static int GetChosenOrderRestoreId()
        {
            var id = Forms["chosenOrderRestoreId"];

            if (id == null || id == string.Empty)
            {
                return 0;
            }

            if (!int.TryParse(id, out int idParsed))
            {
                Message = $"Некорректный id - {id}!";

                return 0;
            }

            return idParsed;
        }

        private static bool AddOrder()
        {
            var idCustomer = Forms["customerId"];
            var idProduct = Forms["chosenProductId"];
            var quantity = Forms["chosenProductQuantity"];
            var adress = Forms["orderAdress"];

            if (idProduct == null || quantity == null || adress == null || idCustomer == null)
            {
                return false;
            }

            if (idProduct == string.Empty || quantity == string.Empty || adress == string.Empty || idCustomer == string.Empty)
            {
                return false;
            }

            return OrderAdd(idCustomer, idProduct, quantity, adress);
        }

        private static bool OrderAdd(string idCustomer, string idProduct, string quantity, string adress)
        {
            if (!int.TryParse(idCustomer, out int idCustomerParsed))
            {
                Message = $"Некорректный idCustomer - '{idCustomer}'!";

                return false;
            }

            if (!int.TryParse(idProduct, out int idProductParsed))
            {
                Message = $"Некорректный idProduct - '{idProduct}'!";

                return false;
            }

            if (!int.TryParse(quantity, out int quantityParsed))
            {
                Message = $"Некорректный quantity - '{quantity}'!";

                return false;
            }

            return OrderAddParsed(idCustomerParsed, idProductParsed, quantityParsed, adress);
        }

        private static bool OrderAddParsed(int idCustomer, int idProduct, int quantity, string adress)
        {
            var listIdProduct = new List<int>
            {
                idProduct
            };

            var product = Dependencies.ProductLogic.GetById(idProduct);

            decimal sum = product.Price * quantity;

            var order = new Order(idCustomer, DateTime.Now, adress, listIdProduct, sum);

            if (Dependencies.OrderLogic.Add(ref order))
            {
                AddProductsToOrder(order);

                Message = "Заказ создан";

                return true;
            }
            else
            {
                Message = "Ошибка создания заказа!";

                return false;
            }
        }

        private static void AddProductsToOrder(Order order)
        {
            foreach (var productId in order.ListIdProduct)
            {
                if (!Dependencies.OrderProductLogic.Add(new OrderProduct(order.Id, productId)))
                {
                    Message = $"Товар id '{productId}' не был добавлен в заказ id '{order.Id}'";

                    return;
                }
            }
        }

        private static User RegisterUser()
        {
            var regName = Forms["regName"];
            var regPass = Forms["regPass"];
            //var regRole = Forms["regRole"];

            if (regName == null || regPass == null || regName == string.Empty || regPass == string.Empty)
            {
                return User.Guest;
            }

            if (CurrentUser == User.Guest)
            {
                return RegisterNewUser(regName, regPass, Role.Customer);
            }

            return User.Guest;
        }

        private static void RegisterCustomer(User user)
        {
            var customer = new Customer(user.Name, user);

            if (Dependencies.CustomerLogic.Add(ref customer))
            {
                Message = "Покупатель зарегистрирован";

                return;
            }
            else
            {
                Message = "Ошибка регистрации покупателя!";

                return;
            }
        }

        private static User RegisterNewUser(string regName, string regPass, Role regRole)
        {
            var userLogic = Dependencies.UserLogic;

            if (userLogic.GetByName(regName) != null)
            {
                Message = "Пользователь с таким именем уже существует!";

                return User.Guest;
            }

            var roleId = Dependencies.RoleLogic.GetIdByName(regRole.Name);

            if (userLogic.Add(roleId, regName, regPass))
            {
                CurrentUser = Dependencies.UserLogic.GetByName(regName);
                Message = "Пользователь зарегистрирован";

                return CurrentUser;
            }
            else
            {
                Message = "Ошибка регистрации пользователя!";

                return User.Guest;
            }
        }

        private static bool LogOut()
        {
            var loggedOut = Forms["loggedOut"];

            if (loggedOut == null || loggedOut != "loggedOut")
            {
                return false;
            }

            CurrentUser = User.Guest;
            Message = string.Empty;

            return true;
        }

        private static bool LogIn()
        {
            var logName = Forms["logName"];
            var logPass = Forms["logPass"];

            if (logName == null || logPass == null)
            {
                return false;
            }

            var userLogic = Dependencies.UserLogic;

            var logUser = userLogic.GetByName(logName);

            if (logUser == null)
            {
                Message = "Пользователя с таким именем не существует!";

                return true;
            }

            if (!userLogic.PasswordIsOk(logPass, logUser.PasswordHash))
            {
                Message = "Неправильный пароль!";

                return true;
            }
            else
            {
                CurrentUser = logUser;
                Message = string.Empty;

                return true;
            }
        }

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