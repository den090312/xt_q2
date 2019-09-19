using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_UI
{
    public class DataBase
    {
        public static bool UserExists(WebUser user)
        {
            return true;
        }

        public static bool UserAdded(WebUser user)
        {
            return true;
        }

        public static string GetPasswordHash(string password)
        {
            NullCheck(password);
            EmptyStringCheck(password);

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