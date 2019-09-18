using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_UI
{
    public static class Authentication
    {
        public static bool LoggedIn(WebUser webUser)
        {
            try
            {
                NullCheck(webUser);

                WebUser.Current = webUser;

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
                WebUser.Current = null;

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
                WebUser.ListWebUsers.Add(webUser);

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