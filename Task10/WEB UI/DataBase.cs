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

            return false;
        }

        public static bool PasswordIsOk(string userName, string password)
        {
            NullCheck(userName);
            EmptyStringCheck(userName);

            NullCheck(password);
            EmptyStringCheck(password);

            return password == GetPasswordByName(userName);
        }

        public static string GetHashFromPassword(string password)
        {
            NullCheck(password);
            EmptyStringCheck(password);

            var hash = password;

            return hash;
        }

        private static string GetPasswordByName(string userName)
        {
            NullCheck(userName);
            EmptyStringCheck(userName);

            var hash = GetHashFromDB(userName);

            NullCheck(hash);
            EmptyStringCheck(hash);

            var password = GetPasswordFromHash(hash);

            NullCheck(password);
            EmptyStringCheck(password);

            return password;
        }

        private static string GetPasswordFromHash(string hash)
        {
            var password = hash;

            return password;
        }

        private static string GetHashFromDB(string userName)
        {
            var hash = userName;

            return hash;
        }

        public static IEnumerable<WebUser> FindUsersInRole(Role role)
        {
            NullCheck(role);

            return new List<WebUser>();
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