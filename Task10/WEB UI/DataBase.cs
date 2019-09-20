using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WEB_UI
{
    public class Database
    {
        private static readonly string connectionString = @"Data Source=DEN090312\SQLEXPRESS;Initial Catalog=webusersdb;Integrated Security=True";

        public static bool UserExists(Webuser webuser)
        {
            return true;
        }

        public static bool UserAdded(Webuser webuser)
        {
            NullCheck(webuser);

            try
            {
                AddWebuser(webuser);

                return true;
            }
            catch
            {
                return false;
            }

            //AddWebuser(webuser);

            //return true;
        }

        public static bool RoleAdded(Role role)
        {
            NullCheck(role);

            try
            {
                AddRole(role);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void AddRole(Role role)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
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

        private static void AddWebuser(Webuser webuser)
        {
            var roleName = webuser?.role?.Name;

            NullCheck(roleName);

            var IdRole = GetIdRoleByName(roleName);

            if (IdRole == -1)
            {
                throw new Exception($"Can't find id role by '{roleName}'!");
            }

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "AddWebuser";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParRoleId(IdRole));
                sqlCommand.Parameters.Add(SqlParWebuserName(webuser));
                sqlCommand.Parameters.Add(SqlParPasswordHash(webuser));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }

        private static SqlParameter SqlParRoleId(int IdRole)
        {
            return new SqlParameter
            {
                ParameterName = "@RoleId",
                Value = IdRole,
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input
            };
        }

        private static SqlParameter SqlParWebuserName(Webuser webuser)
        {
            return new SqlParameter
            {
                ParameterName = "@UserName",
                Value = webuser.Name,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };
        }

        private static SqlParameter SqlParPasswordHash(Webuser webuser)
        {
            return new SqlParameter
            {
                ParameterName = "@PasswordHash",
                Value = webuser.PasswordHash,
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input
            };
        }

        private static int GetIdRoleByName(string roleName)
        {
            int IdRole = -1;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetIdRoleByName";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = roleName,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                });

                sqlCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@IdRole",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                });

                sqlConnection.Open();

                var sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    return (int)sqlDataReader[0];
                }
            }

            return IdRole;
        }

        public static bool UserNameExists(string userName)
        {
            NullCheck(userName);
            EmptyStringCheck(userName);

            return false;
        }

        public static bool PasswordIsOk(string userName, string password)
        {
            NullCheck(userName);
            EmptyStringCheck(userName);

            NullCheck(password);
            EmptyStringCheck(password);

            return password == GetPasswordByName(userName);
        }

        private static string GetPasswordByName(string userName)
        {
            NullCheck(userName);
            EmptyStringCheck(userName);

            var hash = GetHashFromDB(userName);

            NullCheck(hash);
            EmptyStringCheck(hash);

            var password = GetPasswordFromHash(hash);

            NullCheck(password);
            EmptyStringCheck(password);

            return password;
        }

        private static string GetPasswordFromHash(string hash)
        {
            var password = hash;

            return password;
        }

        private static string GetHashFromDB(string userName)
        {
            var hash = userName;

            return hash;
        }

        public static IEnumerable<Webuser> FindUsersInRole(Role role)
        {
            NullCheck(role);

            return new List<Webuser>();
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