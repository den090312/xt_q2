using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_UI
{
    public static class Authentication
    {
        public static WebUser CurrentUser { get; set; } = WebUser.Guest;

        public static bool LoggedIn(WebUser webUser)
        {
            try
            {
                NullCheck(webUser);

                CurrentUser = webUser;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool LoggedOut()
        {
            try
            {
                CurrentUser = null;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Registered(WebUser webUser)
        {
            try
            {
                NullCheck(webUser);

                Register(webUser);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void Register(WebUser webUser)
        {

        }

        private static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject == null)
            {
                throw new NullReferenceException($"{nameof(classObject)} is null!");
            }
        }
    }
}