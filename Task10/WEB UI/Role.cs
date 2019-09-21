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

        private Role(string roleName)
        {
            NullCheck(roleName);
            EmptyStringCheck(roleName);

            Name = roleName;
        }

        public static Role Create(string roleName)
        {
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

        public static void AddUserToRole(Webuser user, Role role)
        {
            NullCheck(user);
            NullCheck(role);

            role.UserList.Add(user);
        }

        public static bool NameExists(string roleName)
        {
            int roleCount = 0;

            roleCount = GetRoleNameCountInDb(roleName, roleCount);

            return roleCount == 1;
        }

        private static int GetRoleNameCountInDb(string roleName, int roleCount)
        {
            using (var sqlConnection = new SqlConnection(Database.ConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "RoleNameCount";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParRoleName(roleName));
                sqlCommand.Parameters.Add(SqlParCountName());

                sqlConnection.Open();

                roleCount = GetRoleNameCount(roleCount, sqlCommand);
            }

            return roleCount;
        }

        private static int GetRoleNameCount(int roleCount, SqlCommand sqlCommand)
        {
            var sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                roleCount = (int)sqlDataReader[0];
            }

            return roleCount;
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

        private static void DeleteWebrole(Role role)
        {
            var roleId = GetRoleId(role);

            using (var sqlConnection = new SqlConnection(Database.ConnectionString))
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
            using (var sqlConnection = new SqlConnection(Database.ConnectionString))
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