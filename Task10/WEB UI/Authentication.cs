using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_UI
{
    public static class Authentication
    {
        public static WebUser CurrentUser { get; set; } = WebUser.Guest;

        public static void LogIn(WebUser user)
        {
            NullCheck(user);

            if (DataBase.UserExists(user))
            {
                CurrentUser = user;
            }
            else
            {
                CurrentUser = WebUser.Guest;
            }
        }

        public static void LogOut() => CurrentUser = WebUser.Guest;

        private static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject == null)
            {
                throw new NullReferenceException($"{nameof(classObject)} is null!");
            }
        }
    }
}