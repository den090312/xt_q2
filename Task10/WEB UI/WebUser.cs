using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WEB_UI
{
    public class Webuser
    {
        public string Name { get; } = string.Empty;

        public Role role { get; }

        private string PasswordHash { get; }

        private readonly static char hashSeparator = '|';

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

        public static Webuser Create(string name, Role role, string password)
        {
            var webuser = new Webuser(name, role, password);

            list.Add(webuser);

            Role.AddUserToRole(webuser, role);

            return webuser;
        }

        public static Webuser GetLoggedUser(string userName, string userPass)
        {
            var roleId = GetRoleIdByUserName(userName);

            var roleName = Role.GetRoleNameByRoleId(roleId);
            var userRole = Role.Get(roleName);

            var webuser = Create(userName, userRole, userPass);

            return webuser;
        }

        private static int GetRoleIdByUserName(string userName)
        {
            var roleId = -1;

            using (var sqlConnection = new SqlConnection(Database.ConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetRoleIdByUserName";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParUserName(userName));
                sqlCommand.Parameters.Add(SqlParRoleId());

                sqlConnection.Open();

                var sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    roleId = (int)sqlDataReader[0];
                }

                if (roleId == -1)
                {
                    throw new Exception($"Can't find role id by '{userName}' in database!");
                }
            }

            return roleId;
        }

        private static string GetHashFromPassword(string password)
        {
            NullCheck(password);
            EmptyStringCheck(password);

            var listPass = new List<string>();
            var step = 4;

            for (var i = 0; i < password.Length; i += step)
            {
                listPass.Add(password.Substring(i, Math.Min(step, password.Length - i)));
            }

            var hashSb = new StringBuilder();

            for (var i = 0; i < listPass.Count; i++)
            {
                var bytes = Encoding.ASCII.GetBytes(AppendSpacesToString(listPass[i], 4));

                hashSb.Append(BitConverter.ToInt32(bytes, 0));

                if (i == listPass.Count - 1)
                {
                    continue;
                }

                hashSb.Append(hashSeparator);
            }

            return hashSb.ToString();
        }

        private static string AppendSpacesToString(string subString, int spaceCount)
        {
            if (subString.Length < spaceCount)
            {
                var sb = new StringBuilder(subString);

                while (sb.Length != spaceCount)
                {
                    sb.Append(" ");
                }

                subString = sb.ToString();
            }

            return subString;
        }

        private static string GetPasswordFromHash(string hash)
        {
            var hashArray = hash.Split(hashSeparator);
            var passSB = new StringBuilder();

            foreach (var subHash in hashArray)
            {
                var hashInt = int.Parse(subHash);
                var bytes = BitConverter.GetBytes(hashInt);
                var subPass = Encoding.ASCII.GetString(bytes);

                passSB.Append(subPass);
            }

            return passSB.ToString().TrimEnd();
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

            var hash = GetHashFromDb(userName);
            var password = GetPasswordFromHash(hash);

            NullCheck(password);
            EmptyStringCheck(password);

            return password;
        }

        private static string GetHashFromDb(string userName)
        {
            var hash = string.Empty;

            using (var sqlConnection = new SqlConnection(Database.ConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetPasswordHashFormUserName";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParUserName(userName));
                sqlCommand.Parameters.Add(SqlParPasswordHash());

                sqlConnection.Open();

                var sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    hash = (string)sqlDataReader[0];
                }

                if (hash == string.Empty)
                {
                    throw new Exception("Can't find password hash in database!");
                }
            }

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

            if (AddedToDb(user))
            {
                Authentication.CurrentUser = user;

                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ExistsInDb(Webuser webuser)
        {
            int userCount = 0;

            userCount = GetUserNameCount(webuser.Name, userCount);

            return userCount == 1;
        }

        public static bool NameExistsInDb(string userName)
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

                sqlCommand.Parameters.Add(SqlParUserName(userName));
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

        public static bool AddedToDb(Webuser webuser)
        {
            NullCheck(webuser);

            try
            {
                AddWebuserToDb(webuser);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void AddWebuserToDb(Webuser webuser)
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
                sqlCommand.Parameters.Add(SqlParUserName(webuser.Name));
                sqlCommand.Parameters.Add(SqlParPasswordHash(webuser.PasswordHash));

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

        private static SqlParameter SqlParRoleId()
        {
            return new SqlParameter
            {
                ParameterName = "@RoleId",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
        }

        private static SqlParameter SqlParUserName(string userName)
        {
            return new SqlParameter
            {
                ParameterName = "@UserName",
                Value = userName,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };
        }

        private static SqlParameter SqlParPasswordHash(string passwordHash)
        {
            return new SqlParameter
            {
                ParameterName = "@PasswordHash",
                Value = passwordHash,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };
        }

        private static SqlParameter SqlParPasswordHash()
        {
            return new SqlParameter
            {
                ParameterName = "@PasswordHash",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Output,
                Size = 500
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