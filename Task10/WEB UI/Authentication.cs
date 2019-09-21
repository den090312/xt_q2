using System;

namespace WEB_UI
{
    public static class Authentication
    {
        public static Webuser CurrentUser { get; set; }

        static Authentication() => CurrentUser = Webuser.Guest;

        public static bool LoggedIn(Webuser webuser)
        {
            NullCheck(webuser);

            if (Webuser.ExistsInDB(webuser))
            {
                CurrentUser = webuser;

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