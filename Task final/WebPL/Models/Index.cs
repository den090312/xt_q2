using Entities;
using Common;
using System.Collections.Specialized;
using System;
using System.Linq;
using log4net.Repository.Hierarchy;
using log4net.Appender;
using System.IO;
using System.Text;

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
            if (Account() || Order() || Product() || Manager())
            {
                return;
            }
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
            var manager = new Entities.Manager(userManagerId, "Manager", Entities.Manager.Rank.General);

            Dependencies.ManagerLogic.Add(ref manager);
        }

        public static string GetLastError()
        {
            var logPath = GetLogPath();

            NullCheck(logPath);
            EmptyStringCheck(logPath);

            var fs = new FileStream(logPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var encoding = Encoding.GetEncoding(1251);

            var lastError = string.Empty;
            
            using (var sr = new StreamReader(fs, encoding))
            {
                while (!sr.EndOfStream)
                {
                    lastError = sr.ReadLine();
                }
            }

            return lastError;
        }

        private static string GetLogPath()
        {
            var repository = Dependencies.LoggerLogic.Log.Logger.Repository;
            var appenders = ((Hierarchy)repository).Root.Appenders.OfType<RollingFileAppender>();

            var logPath = string.Empty;

            foreach (var appender in appenders)
            {
                logPath = appender.File;
            }

            return logPath;
        }

        private static bool Account()
        {
            if (!Models.Account.Run(Forms))
            {
                return false;
            }

            var message = Models.Account.Message;

            if (message != string.Empty)
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

            return true;
        }

        private static bool Order()
        {
            if (!Models.Order.Run(Forms))
            {
                return false;
            }

            var message = Models.Order.Message;

            if (message != string.Empty)
            {
                Message = message;
            }

            return true;
        }

        private static bool Product()
        {
            if (!Models.Product.Run(Forms))
            {
                return false;
            }

            var message = Models.Product.Message;

            if (message != string.Empty)
            {
                Message = message;
            }

            return true;
        }

        private static bool Manager()
        {
            if (!Models.Manager.Run(Forms))
            {
                return false;
            }

            var message = Models.Manager.Message;

            if (message != string.Empty)
            {
                Message = message;
            }

            return true;
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

        private static void EmptyStringCheck(string inputString)
        {
            if (inputString == string.Empty)
            {
                throw new ArgumentException($"{nameof(inputString)} is empty!");
            }
        }

        private static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
    }
}