using System;
using System.Collections.Generic;

namespace WEB_UI
{
    public class WebUser
    {
        public int Id { get; }

        public string Name { get; } = string.Empty;

        public Role UserRole { get; } = new Role("Guest");

        public string PasswordHash { get; } = string.Empty;

        public static List<WebUser> WebUserList { get; }

        public readonly static WebUser Guest;

        public readonly static WebUser User;

        public readonly static WebUser Admin;

        static WebUser()
        {
            Guest = new WebUser("Guest", new Role("Guest"), "Guest");
            User = new WebUser("User", new Role("User"), "User");
            Admin = new WebUser("Admin", new Role("Admin"), "Admin");

            WebUserList = new List<WebUser>
            {
                Guest,
                User,
                Admin
            };
        }

        public WebUser(string name, Role role, string password)
        {
            NullCheck(name);
            NullCheck(role);
            NullCheck(password);

            EmptyStringCheck(name);
            EmptyStringCheck(password);

            Name = name;
            UserRole = role;

            var passwordHash = Database.GetHashFromPassword(password);

            NullCheck(passwordHash);
            EmptyStringCheck(passwordHash);

            PasswordHash = passwordHash;

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