using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_UI
{
    public class Role
    {
        public string Name { get; } = string.Empty;

        private static readonly List<Role> roleList;

        static Role() => roleList = new List<Role>();

        public List<WebUser> listUsers;

        private Role(string roleName)
        {
            NullCheck(roleName);
            EmptyStringCheck(roleName);

            Name = roleName;
        }

        public static void AddUsersToRoles(IEnumerable<WebUser> users, IEnumerable<Role> roles)
        {
            foreach (var role in roles)
            {
                AddUsersToRole(users, role);
            }
        }

        public static void AddUsersToRole(IEnumerable<WebUser> users, Role role)
        {
            NullCheck(users);
            NullCheck(role);

            foreach (var user in users)
            {
                AddUserToRole(user, role);
            }
        }

        public static void AddUserToRole(WebUser user, Role role)
        {
            NullCheck(user);
            NullCheck(role);

            role.listUsers.Add(user);
        }

        public static void CreateRole(string roleName)
        {
            var newRole = new Role(roleName);

            if (!roleList.Exists(role => role.Name == roleName))
            {
                roleList.Add(newRole);
            }
        }

        public static void DeleteRole(Role role) => roleList.Remove(role);

        public Role GetRole(string roleName)
        {
            NullCheck(roleName);
            EmptyStringCheck(roleName);

            return roleList.Find(role => role.Name == roleName);
        }

        public static IEnumerable<WebUser> FindUsersInRole(Role role)
        {
            NullCheck(role);

            return Database.FindUsersInRole(role);
        }

        public static IEnumerable<Role> GetAllRoles() => roleList;

        public static bool TryGetRoleForUser(WebUser user, out Role userRole)
        {
            NullCheck(user);

            if (!roleList.Exists(role => role.Name == user.Name))
            {
                userRole = null;

                return false;
            }
            else
            {
                userRole = roleList.Find(role => role.Name == user.Name);

                return true;
            }
        }

        public static IEnumerable<WebUser> GetUsersInRole(Role userRole)
        {
            NullCheck(userRole);

            return WebUser.WebUserList.FindAll(role => role.Role == userRole);
        }

        public static bool IsUserInRole(WebUser user, Role role)
        {
            NullCheck(user);
            NullCheck(role);

            return user.Role == role;
        }

        public static void RemoveUsersFromRoles(IEnumerable<WebUser> users, IEnumerable<Role> roles)
        {
            NullCheck(users);
            NullCheck(roles);

            foreach (var role in roles)
            {
                RemoveUsersFromRole(users, role);
            }
        }

        public static void RemoveUsersFromRole(IEnumerable<WebUser> users, Role role)
        {
            NullCheck(users);
            NullCheck(role);

            foreach (var user in users)
            {
                RemoveUserFromRole(role, user);
            }
        }

        public static void RemoveUserFromRole(Role role, WebUser user)
        {
            NullCheck(role);
            NullCheck(user);

            role.listUsers.Remove(user);
        }

        public static bool RoleExists(Role userRole)
        {
            NullCheck(userRole);

            return roleList.Exists(role => role == userRole);
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