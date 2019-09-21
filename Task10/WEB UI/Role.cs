using System;
using System.Collections.Generic;
using System.Data;
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

        public static Role Create(string roleName)
        {
            var newRole = new Role(roleName);

            if (NameExistsInDB(roleName))
            {
                AddNewRoleToList(newRole);

                return Get(roleName);
            }

            if (AddedInDB(newRole))
            {
                AddNewRoleToList(newRole);
            }
            else
            {
                throw new Exception("Error adding new role to database!");
            }

            return newRole;
        }

        public static Role Get(string roleName)
        {
            if (!NameExistsInDB(roleName))
            {
                return Create(roleName);
            }

            if (roleName.ToLower() == "guest")
            {
                return new Role("Guest");
            }

            bool matchName(Role role) => role.Name.ToLower() == roleName.ToLower();

            return RoleList.Find(matchName);
        }

        private static void AddNewRoleToList(Role newRole)
        {
            bool matchName(Role role) => role.Name.ToLower() == newRole.Name.ToLower();

            if (!RoleList.Exists(matchName))
            {
                if (newRole.Name.ToLower() != "guest")
                {
                    RoleList.Add(newRole);
                }
            }
        }

        public static bool NameExistsInDB(string roleName)
        {
            int roleCount = 0;

            roleCount = GetRoleNameCount(roleName, roleCount);

            return roleCount == 1;
        }

        private static int GetRoleNameCount(string roleName, int roleCount)
        {
            using (var sqlConnection = new SqlConnection(Database.ConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "RoleNameCount";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@RoleName",
                    Value = roleName,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                });

                sqlCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@Count",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                });

                sqlConnection.Open();

                var sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    roleCount = (int)sqlDataReader[0];
                }
            }

            return roleCount;
        }

        public static void Delete(Role role)
        {
            if (DeletedFromDB(role))
            {
                RoleList.Remove(role);
            }
            else
            {
                throw new Exception("Error role delete!");
            }
        }

        public Role GetRoleFromList(string roleName)
        {
            NullCheck(roleName);
            EmptyStringCheck(roleName);

            bool matchRole(Role role) => role.Name == roleName;

            return RoleList.Find(matchRole);
        }

        public static IEnumerable<Webuser> FindUsersInRole(Role role)
        {
            NullCheck(role);

            return Webuser.FindUsersInRole(role);
        }

        public static IEnumerable<Role> GetAll() => RoleList;

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

        public static bool WebroleExists(Role userRole)
        {
            NullCheck(userRole);

            bool matchRole(Role role) => role == userRole;

            return RoleList.Exists(matchRole);
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

        public static bool AddedInDB(Role role)
        {
            NullCheck(role);

            try
            {
                AddWebroleToDB(role);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeletedFromDB(Role role)
        {
            NullCheck(role);

            try
            {
                DeleteWebrole(role);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void DeleteWebrole(Role role)
        {
            var roleId = GetRoleId(role);

            using (var sqlConnection = new SqlConnection(Database.ConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "DeleteWebrole";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@RoleId",
                    Value = roleId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                });

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }

        private static void AddWebroleToDB(Role role)
        {
            using (var sqlConnection = new SqlConnection(Database.ConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "AddWebrole";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@RoleName",
                    Value = role.Name,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                });

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }

        public static int GetRoleId(Role role) => GetIdRoleByName(role.Name);

        private static int GetIdRoleByName(string roleName)
        {
            int IdRole = -1;

            using (var sqlConnection = new SqlConnection(Database.ConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetIdRoleByName";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParRoleName(roleName));
                sqlCommand.Parameters.Add(SqlParIdRole());

                sqlConnection.Open();
                IdRole = GetIdRole(roleName, IdRole, sqlCommand);
            }

            return IdRole;
        }

        private static int GetIdRole(string roleName, int IdRole, SqlCommand sqlCommand)
        {
            var sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                IdRole = (int)sqlDataReader[0];
            }

            if (IdRole == -1)
            {
                throw new Exception($"Can't find id role by '{roleName}'!");
            }

            return IdRole;
        }

        private static SqlParameter SqlParIdRole()
        {
            return new SqlParameter
            {
                ParameterName = "@IdRole",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
        }

        private static SqlParameter SqlParRoleName(string roleName)
        {
            return new SqlParameter
            {
                ParameterName = "@RoleName",
                Value = roleName,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };
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