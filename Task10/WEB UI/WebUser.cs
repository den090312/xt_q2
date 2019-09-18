using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_UI
{
    public class WebUser
    {
        public static WebUser Current { get; set; } = Guest;

        public static List<WebUser> ListWebUsers { get; }

        public string Name { get; }

        public Role UserRole { get; } = Role.None;

        public string PasswordHash { get; }

        public static WebUser Guest => new WebUser("Guest", Role.Guest, "Guest");

        public static WebUser Admin => new WebUser("admin", Role.Admin, "admin");

        public static WebUser User => new WebUser("user", Role.User, "user");

        public enum Role
        {
            None = 0,
            Guest = 1,
            User = 2,
            Admin = 3
        }

        public WebUser(string name, Role userRole, string password)
        {
            NullCheck(name);
            NullCheck(password);

            EmptyStringCheck(name);
            EmptyStringCheck(password);

            Name = name;
            UserRole = userRole;
            PasswordHash = GetPasswordHash(password);
        }

        private string GetPasswordHash(string password)
        {
            NullCheck(password);

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