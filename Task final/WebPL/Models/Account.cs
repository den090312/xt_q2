using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace WebPL.Models
{
    public class Account
    {
        private static NameValueCollection forms;

        public static string Message { get; private set; }

        public static User CurrentUser { get; private set; }

        static Account() => CurrentUser = User.Guest;

        public static void Run(NameValueCollection forms)
        {
            Account.forms = forms;

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

        private static bool LogIn()
        {
            var logName = forms["logName"];
            var logPass = forms["logPass"];

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

        private static bool LogOut()
        {
            var loggedOut = forms["loggedOut"];

            if (loggedOut == null || loggedOut != "loggedOut")
            {
                return false;
            }

            CurrentUser = User.Guest;
            Message = string.Empty;

            return true;
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

        private static User RegisterUser()
        {
            var regName = forms["regName"];
            var regPass = forms["regPass"];
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
    }
}