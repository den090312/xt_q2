using System;
using System.Collections.Generic;

namespace WEB_UI
{
    public class WebUser
    {
        public int Id { get; }

        public string Name { get; } = string.Empty;

        public Role role { get; }

        public string PasswordHash { get; } = string.Empty;

        public readonly static WebUser Guest;

        public static List<WebUser> list { get; }

        static WebUser()
        {
            Guest = new WebUser("Guest", new Role("Guest"), "Guest");
            list = new List<WebUser>
            {
                Guest
            };
        }

        public WebUser()
        {
        }

        private WebUser(string name, Role role, string password)
        {
            NullCheck(name);
            NullCheck(role);
            NullCheck(password);

            EmptyStringCheck(name);
            EmptyStringCheck(password);

            Name = name;
            this.role = role;

            var passwordHash = Database.GetHashFromPassword(password);

            NullCheck(passwordHash);
            EmptyStringCheck(passwordHash);

            PasswordHash = passwordHash;
        }

        public WebUser Create(string name, Role role, string password)
        {
            var webUser = new WebUser(name, role, password);

            list.Add(webUser);

            return webUser;
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