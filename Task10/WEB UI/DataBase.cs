using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WEB_UI
{
    public class Database
    {
        public static bool UserExists(Webuser webuser)
        {
            return true;
        }

        public static bool UserAdded(Webuser webuser)
        {
            NullCheck(webuser);

            try
            {
                AddWebuserToDatabase(webuser);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void AddWebuserToDatabase(Webuser webuser)
        {
            var IdRole = GetIdRoleByName(webuser?.role?.Name);

            using (var sqlConnection = new SqlConnection())
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
            var IdRole = 1;

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

        public static int GetHashFromPassword(string password)
        {
            NullCheck(password);
            EmptyStringCheck(password);

            var hash = 0;

            return hash;
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