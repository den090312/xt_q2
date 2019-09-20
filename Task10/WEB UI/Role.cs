using System;
using System.Collections.Generic;

namespace WEB_UI
{
    public class Role
    {
        public string Name { get; } = string.Empty;

        private List<Webuser> userList = new List<Webuser>();

        public static List<Role> RoleList { get; }

        static Role() => RoleList = new List<Role>();

        private Role(string roleName)
        {
            NullCheck(roleName);
            EmptyStringCheck(roleName);

            Name = roleName;
        }

        public static void AddUsersToRoles(IEnumerable<Webuser> users, IEnumerable<Role> roles)
        {
            foreach (var role in roles)
            {
                AddUsersToRole(users, role);
            }
        }

        public static void AddUsersToRole(IEnumerable<Webuser> users, Role role)
        {
            NullCheck(users);
            NullCheck(role);

            foreach (var user in users)
            {
                AddUserToRole(user, role);
            }
        }

        public static void AddUserToRole(Webuser user, Role role)
        {
            NullCheck(user);
            NullCheck(role);

            role.userList.Add(user);
        }

        public static Role Create(string roleName)
        {
            var newRole = new Role(roleName);

            if (roleName.ToLower() != "guest")
            {
                if (!RoleList.Exists(role => role.Name == roleName))
                {
                    RoleList.Add(newRole);
                }
            }

            return newRole;
        }

        public static void DeleteRole(Role role) => RoleList.Remove(role);

        public Role GetRole(string roleName)
        {
            NullCheck(roleName);
            EmptyStringCheck(roleName);

            return RoleList.Find(role => role.Name == roleName);
        }

        public static IEnumerable<Webuser> FindUsersInRole(Role role)
        {
            NullCheck(role);

            return Database.FindUsersInRole(role);
        }

        public static IEnumerable<Role> GetAllRoles() => RoleList;

        public static bool TryGetRoleForUser(Webuser user, out Role userRole)
        {
            NullCheck(user);

            if (!RoleList.Exists(role => role.Name == user.Name))
            {
                userRole = null;

                return false;
            }
            else
            {
                userRole = RoleList.Find(role => role.Name == user.Name);

                return true;
            }
        }

        public static IEnumerable<Webuser> GetUsersInRole(Role userRole)
        {
            NullCheck(userRole);

            return Webuser.list.FindAll(role => role.role == userRole);
        }

        public static bool IsUserInRole(Webuser user, Role role)
        {
            NullCheck(user);
            NullCheck(role);

            return user.role == role;
        }

        public static void RemoveUsersFromRoles(IEnumerable<Webuser> users, IEnumerable<Role> roles)
        {
            NullCheck(users);
            NullCheck(roles);

            foreach (var role in roles)
            {
                RemoveUsersFromRole(users, role);
            }
        }

        public static void RemoveUsersFromRole(IEnumerable<Webuser> users, Role role)
        {
            NullCheck(users);
            NullCheck(role);

            foreach (var user in users)
            {
                RemoveUserFromRole(role, user);
            }
        }

        public static void RemoveUserFromRole(Role role, Webuser user)
        {
            NullCheck(role);
            NullCheck(user);

            role.userList.Remove(user);
        }

        public static bool RoleExists(Role userRole)
        {
            NullCheck(userRole);

            return RoleList.Exists(role => role == userRole);
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

        public override bool Equals(object obj)
        {
            NullCheck(obj);

            return obj is Role role &&
                   Name == role.Name &&
                   EqualityComparer<List<Webuser>>.Default.Equals(userList, role.userList);
        }

        public override int GetHashCode()
        {
            var hashCode = -513442300;

            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Webuser>>.Default.GetHashCode(userList);

            return hashCode;
        }

        public static bool operator ==(Role role1, Role role2)
        {
            NullCheck(role1);
            NullCheck(role2);

            return role1.Name == role2.Name;
        }

        public static bool operator !=(Role role1, Role role2)
        {
            NullCheck(role1);
            NullCheck(role2);

            return role1.Name != role2.Name;
        }
    }
}