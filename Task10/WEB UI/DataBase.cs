using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_UI
{
    public class Database
    {
        public static bool UserExists(WebUser user)
        {
            return true;
        }

        public static bool UserAdded(WebUser user)
        {
            return true;
        }

        public static bool UserNameExists(string userName)
        {
            NullCheck(userName);
            EmptyStringCheck(userName);

            return true;
        }

        public static bool PasswordIsOk(string userName, string password) => password == GetPassWordByName(userName);

        public static string GetHashFromPassword(string password)
        {
            NullCheck(password);
            EmptyStringCheck(password);

            var hash = string.Empty;

            return hash;
        }

        private static string GetPasswordFromHash(string hash)
        {
            var password = string.Empty;

            return password;
        }

        private static string GetPassWordByName(string userName)
        {
            NullCheck(userName);
            EmptyStringCheck(userName);

            var password = string.Empty;

            return password;
        }

        private static void EmptyStringCheck(string inputString)
        {
            if (inputString == string.Empty)
            {
                throw new Exception($"{nameof(inputString)} is empty!");
            }
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