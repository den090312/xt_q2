using Common;
using Entities;
using System.Collections.Specialized;

namespace WebPL.Models
{
    public class Account
    {
        private static NameValueCollection forms;

        public static string Message { get; private set; }

        public static User CurrentUser { get; private set; }

        static Account()
        {
            Message = string.Empty;
            CurrentUser = User.Guest;
        }

        public static bool Run(NameValueCollection forms)
        {
            Account.forms = forms;

            if (TryLogIn() || TryLogOut() || TryChangePassword())
            {
                return true;
            }

            var registerUser = RegisterUser();

            if (registerUser == User.Guest)
            {
                return false;
            }

            RegisterCustomer(registerUser);

            return true;
        }

        private static bool TryChangePassword()
        {
            var currentUser = Index.CurrentUser;

            if (currentUser == User.Guest)
            {
                return false;
            }

            var oldPass = forms["oldPass"];
            var newPass = forms["newPass"];

            if (string.IsNullOrEmpty(oldPass) || string.IsNullOrEmpty(newPass))
            {
                return false;
            }

            return PasswordChange(currentUser, oldPass, newPass);
        }

        private static bool PasswordChange(User currentUser, string oldPass, string newPass)
        {
            var userLogic = Dependencies.UserLogic;

            if (!userLogic.PasswordIsOk(oldPass, currentUser.PasswordHash))
            {
                Message = "Неправильный пароль!";

                return true;
            }
            else if (userLogic.ChangePassword(currentUser, newPass))
            {
                Message = "Пароль изменен";
            }
            else
            {
                Message = "Ошибка изменения пароля!";
            }

            return true;
        }

        private static bool TryLogIn()
        {
            var logName = forms["logName"];
            var logPass = forms["logPass"];

            if (string.IsNullOrEmpty(logName) || string.IsNullOrEmpty(logPass))
            {
                return false;
            }

            return UserLogIn(logName, logPass);
        }

        private static bool UserLogIn(string logName, string logPass)
        {
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
            }
            else
            {
                CurrentUser = logUser;
                Message = "ok";
            }

            return true;
        }

        private static bool TryLogOut()
        {
            var loggedOut = forms["loggedOut"];

            if (loggedOut == null || loggedOut != "loggedOut")
            {
                return false;
            }

            CurrentUser = User.Guest;
            Message = "ok";

            return true;
        }

        private static void RegisterCustomer(User user)
        {
            var customer = new Customer(user.Name, user);

            if (Dependencies.CustomerLogic.Add(ref customer))
            {
                Message = "Покупатель зарегистрирован";

                CurrentUser = user;
            }
            else
            {
                Message = $"Ошибка регистрации покупателя, имя - '{customer.Name}'!";
            }
        }

        private static User RegisterUser()
        {
            var regName = forms["regName"];
            var regPass = forms["regPass"];

            if (string.IsNullOrEmpty(regName) || string.IsNullOrEmpty(regPass))
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
                Message = $"Ошибка. Пользователь с таким именем уже существует - '{regName}'!";

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
                Message = $"Ошибка регистрации пользователя, имя - '{regName}'!";

                return User.Guest;
            }
        }
    }
}