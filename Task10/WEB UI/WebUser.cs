using System;
using System.Collections.Generic;

namespace WEB_UI
{
    public class Webuser
    {
        public string Name { get; } = string.Empty;

        public Role role { get; }

        public int PasswordHash { get; }

        public readonly static Webuser Guest;

        public static List<Webuser> list { get; }

        static Webuser()
        {
            Guest = new Webuser("Guest", Role.Create("Guest"), "Guest");
            list = new List<Webuser>
            {
                Guest
            };
        }

        public Webuser()
        {
        }

        private Webuser(string name, Role role, string password)
        {
            NullCheck(name);
            NullCheck(role);
            NullCheck(password);

            EmptyStringCheck(name);
            EmptyStringCheck(password);

            Name = name;
            this.role = role;

            PasswordHash = Database.GetHashFromPassword(password);
        }

        public static Webuser Create(string name, Role role, string password)
        {
            var webuser = new Webuser(name, role, password);

            list.Add(webuser);

            Role.AddUserToRole(webuser, role);

            return webuser;
        }

        public static bool Registered(Webuser user)
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