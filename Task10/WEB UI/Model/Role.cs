using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WEB_UI
{
    public class Role
    {
        public string Name { get; } = string.Empty;

        public List<Webuser> UserList { get; } = new List<Webuser>();

        public static List<Role> List { get; }

        static Role() => List = new List<Role>();

        private Role(string roleName) => Name = roleName;

        public static Role Create(string roleName)
        {
            NullCheck(roleName);
            EmptyStringCheck(roleName);

            var newRole = new Role(roleName);

            if (NameExists(roleName))
            {
                AddNewRoleToList(newRole);

                return Get(roleName);
            }

            if (Add(newRole))
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
            NullCheck(roleName);
            EmptyStringCheck(roleName);

            if (!NameExists(roleName))
            {
                return Create(roleName);
            }

            if (roleName.ToLower() == "guest")
            {
                return new Role("Guest");
            }

            bool matchName(Role role) => role.Name.ToLower() == roleName.ToLower();

            return List.Find(matchName);
        }

        public static bool Add(Role role)
        {
            NullCheck(role);

            try
            {
                AddWebrole(role);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Delete(Role role)
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

        public static void AddUserToRole(Webuser user, Role role)
        {
            NullCheck(user);
            NullCheck(role);

            role.UserList.Add(user);
        }

        public static bool NameExists(string roleName)
        {
            NullCheck(roleName);
            EmptyStringCheck(roleName);

            return GetRoleNameCount(roleName) == 1;
        }

        private static void AddNewRoleToList(Role newRole)
        {
            bool matchName(Role role) => role.Name.ToLower() == newRole.Name.ToLower();

            if (!List.Exists(matchName))
            {
                if (newRole.Name.ToLower() != "guest")
                {
                    List.Add(newRole);
                }
            }
        }

        private static int GetRoleNameCount(string roleName)
        {
            var sqlConnection = new SqlConnection(Database.WebUiConnectionString);
            var sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = "RoleNameCount";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(SqlParRoleName(roleName));
            sqlCommand.Parameters.Add(SqlParCountName());

            sqlConnection.Open();

            return GetRoleNameCount(sqlCommand.ExecuteReader());
        }

        private static int GetRoleNameCount(SqlDataReader sqlDr)
        {
            var roleCount = 0;

            while (sqlDr.Read())
            {
                roleCount = sqlDr.GetInt32(0);
            }

            return roleCount;
        }

        private static void DeleteWebrole(Role role)
        {
            var roleId = GetRoleId(role);

            using (var sqlConnection = new SqlConnection(Database.WebUiConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "DeleteWebrole";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(SqlParRoleId(roleId));
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        private static void AddWebrole(Role role)
        {
            using (var sqlConnection = new SqlConnection(Database.WebUiConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "AddWebrole";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(SqlParRoleName(role.Name));
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        private static int GetRoleId(Role role) => GetIdRoleByName(role.Name);

        private static int GetIdRoleByName(string roleName)
        {
            using (var sqlConnection = new SqlConnection(Database.WebUiConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetIdRoleByName";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParRoleName(roleName));
                sqlCommand.Parameters.Add(SqlParIdRole());

                sqlConnection.Open();

                return GetIdRole(roleName, sqlCommand.ExecuteReader());
            }
        }

        private static int GetIdRole(string roleName, SqlDataReader sqlDr)
        {
            var IdRole = -1;

            while (sqlDr.Read())
            {
                return sqlDr.GetInt32(0);
            }

            if (IdRole == -1)
            {
                throw new Exception($"Can't find id role by '{roleName}'!");
            }

            return IdRole;
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

        public override bool Equals(object obj)
        {
            NullCheck(obj);

            return obj is Role role &&
                   Name == role.Name &&
                   EqualityComparer<List<Webuser>>.Default.Equals(UserList, role.UserList);
        }

        public override int GetHashCode()
        {
            var hashCode = -513442300;

            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Webuser>>.Default.GetHashCode(UserList);

            return hashCode;
        }

        private static SqlParameter SqlParCountName()
        {
            return new SqlParameter
            {
                ParameterName = "@Count",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
        }

        private static SqlParameter SqlParRoleId(int roleId)
        {
            return new SqlParameter
            {
                ParameterName = "@RoleId",
                Value = roleId,
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input
            };
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