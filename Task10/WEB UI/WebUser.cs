using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_UI
{
    public class WebUser
    {
        public int Id { get; }

        public string Name { get; }

        public Role Role { get; }

        public string PasswordHash { get; }

        public static List<WebUser> WebUserList { get; }

        static WebUser() => WebUserList = new List<WebUser>();

        public WebUser(string name, Role role, string password)
        {
            NullCheck(name);
            NullCheck(role);
            NullCheck(password);

            EmptyStringCheck(name);
            EmptyStringCheck(password);

            Name = name;
            Role = role;
            PasswordHash = Database.GetHashFromPassword(password);

            WebUserList.Add(this);
        }

        public static bool Registered(WebUser user)
        {
            NullCheck(user);

            if (Database.UserAdded(user))
            {
                Authentication.CurrentUser = user;

                return true;
            }
            else
            {
                return false;
            }
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