using System;

namespace WEB_UI
{
    public static class Authentication
    {
        public static Webuser CurrentUser { get; set; }

        static Authentication() => CurrentUser = Webuser.Guest;

        public static bool LoggedIn(Webuser user)
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

        public static void LogOut() => CurrentUser = Webuser.Guest;

        private static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject == null)
            {
                throw new NullReferenceException($"{nameof(classObject)} is null!");
            }
        }
    }
}