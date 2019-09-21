using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WEB_UI
{
    public class Webuser
    {
        public string Name { get; } = string.Empty;

        public Role role { get; }

        private int PasswordHash { get; }

        public readonly static Webuser Guest;

        public static List<Webuser> list { get; }

        static Webuser()
        {
            Guest = new Webuser("Guest", Role.Get("Guest"), "Guest");
            list = new List<Webuser>
            {
                Guest
            };
        }

        private Webuser()
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

            PasswordHash = GetHashFromPassword(password);
        }

        private static int GetHashFromPassword(string password)
        {
            NullCheck(password);
            EmptyStringCheck(password);

            var hash = 0;

            return hash;
        }

        public static Webuser Create(string name, Role role, string password)
        {
            var webuser = new Webuser(name, role, password);

            list.Add(webuser);

            Role.AddUserToRole(webuser, role);

            return webuser;
        }

        public static Webuser Get(string userName, string userPass)
        {
            var userPassHash = GetHashFromPassword(userPass);

            bool matchUser(Webuser user) => user.Name == userName & user.PasswordHash == userPassHash;

            return list.Find(matchUser);
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

            var hash = GetPasswordHashFromDB(userName);

            var password = GetPasswordFromHash(hash);

            NullCheck(password);
            EmptyStringCheck(password);

            return password;
        }

        private static string GetPasswordFromHash(int hash)
        {
            var password = hash.ToString();

            return password;
        }

        private static int GetPasswordHashFromDB(string userName)
        {
            var hash = 0;

            return hash;
        }

        public static IEnumerable<Webuser> FindUsersInRole(Role role)
        {
            NullCheck(role);

            bool matchUsers(Webuser users) => users.role == role;

            return list.FindAll(matchUsers);
        }

        public static bool Registered(Webuser user)
        {
            NullCheck(user);

            if (AddedToDB(user))
            {
                Authentication.CurrentUser = user;

                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ExistsInDB(Webuser webuser)
        {
            int userCount = 0;

            userCount = GetUserNameCount(webuser.Name, userCount);

            return userCount == 1;
        }

        public static bool NameExistsInDB(string userName)
        {
            int userCount = 0;

            userCount = GetUserNameCount(userName, userCount);

            return userCount == 1;
        }

        private static int GetUserNameCount(string userName, int userCount)
        {
            using (var sqlConnection = new SqlConnection(Database.ConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "UserNameCount";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@UserName",
                    Value = userName,
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
                    userCount = (int)sqlDataReader[0];
                }
            }

            return userCount;
        }

        public static bool AddedToDB(Webuser webuser)
        {
            NullCheck(webuser);

            try
            {
                AddWebuserToDB(webuser);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void AddWebuserToDB(Webuser webuser)
        {
            var role = webuser?.role;

            NullCheck(role);

            var roleId = Role.GetRoleId(role);

            using (var sqlConnection = new SqlConnection(Database.ConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "AddWebuser";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParRoleId(roleId));
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