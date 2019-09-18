using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_UI
{
    public class WebUser
    {
        public enum Role
        {
            None = 0,
            Guest = 1,
            User = 2,
            Admin = 3
        }

        public string Name { get; }

        public Role UserRole { get; }

        public string PasswordHash { get; }

        public WebUser(string name, Role userRole, string password)
        {
            NullCheck(name);

            Name = name;
            UserRole = userRole;
            PasswordHash = GetPasswordHash(password); 
        }

        private string GetPasswordHash(string password)
        {
            return password;
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