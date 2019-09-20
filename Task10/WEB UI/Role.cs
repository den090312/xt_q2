using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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

        public static Role Get(string roleName)
        {
            var newRole = new Role(roleName);

            bool matchName(Role role) => roleName.ToLower() == roleName.ToLower();

            if (Database.RoleNameReserved(roleName) & RoleList.Exists(matchName))
            {
                return RoleList.Find(matchName);
            }

            if (!Database.RoleNameReserved(roleName) & RoleList.Exists(matchName))
            {
                if (Database.RoleAdded(newRole))
                {
                    RoleList.Add(newRole);
                }
            }

            if (Database.RoleNameReserved(roleName) & !RoleList.Exists(matchName))
            {
                RoleList.Add(newRole);
            }

            if (!Database.RoleNameReserved(roleName) & !RoleList.Exists(matchName))
            {
                RoleList.Add(newRole);
            }

            if (Database.RoleAdded(newRole))
            {
                RoleList.Add(newRole);
            }
            else
            {
                throw new Exception("Error adding new role to database!");
            }

            return newRole;
        }

        public static void DeleteRole(Role role) => RoleList.Remove(role);

        public Role GetRole(string roleName)
        {
            NullCheck(roleName);
            EmptyStringCheck(roleName);

            bool matchRole(Role role) => role.Name == roleName;

            return RoleList.Find(matchRole);
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

            bool matchName(Role role) => role.Name.ToLower() == user.Name.ToLower();

            if (!RoleList.Exists(matchName))
            {
                userRole = null;

                return false;
            }
            else
            {
                userRole = RoleList.Find(matchName);

                return true;
            }
        }

        public static IEnumerable<Webuser> GetUsersInRole(Role userRole)
        {
            NullCheck(userRole);

            bool matchRole(Webuser role) => role.role == userRole;

            return Webuser.list.FindAll(matchRole);
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

            bool matchRole(Role role) => role == userRole;

            return RoleList.Exists(matchRole);
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