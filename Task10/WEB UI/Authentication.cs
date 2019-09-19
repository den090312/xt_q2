using System;

namespace WEB_UI
{
    public static class Authentication
    {
        public static WebUser CurrentUser { get; set; }

        static Authentication() => CurrentUser = WebUser.Guest;

        public static bool LoggedIn(WebUser user)
        {
            NullCheck(user);

            if (Database.UserExists(user))
            {
                CurrentUser = user;

                return true;
            }
            else
            {
                return false;
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