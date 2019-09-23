using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WEB_UI
{
    public class Webuser
    {
        private string PasswordHash { get; } = string.Empty;

        private readonly static char hashSeparator = '|';

        public readonly static Webuser Guest;

        public string Name { get; } = string.Empty;

        public Role Role { get; }

        public static List<Webuser> List { get; }

        public static Webuser Current { get; private set; }

        static Webuser()
        {
            Guest = new Webuser("Guest", Role.Get("Guest"), "Guest");
            Current = Guest;
            List = new List<Webuser>
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
            Role = role;

            PasswordHash = GetHashFromPassword(password);
        }

        internal static void LogOut() => Current = Guest;

        internal static bool LogIn(string name, string password)
        {
            NullCheck(name);
            NullCheck(password);

            EmptyStringCheck(name);
            EmptyStringCheck(password);

            if (!PasswordIsOk(name, password))
            {
                return false;
            }

            try
            {
                Current = GetLoggedUser(name, password);

                return true;
            }
            catch
            {
                LogOut();

                return false;
            }
        }

        internal static Webuser Create(string name, Role role, string password)
        {
            var webuser = new Webuser(name, role, password);

            List.Add(webuser);

            Role.AddUserToRole(webuser, role);

            return webuser;
        }

        internal static bool PasswordIsOk(string userName, string password)
        {
            NullCheck(userName);
            EmptyStringCheck(userName);

            NullCheck(password);
            EmptyStringCheck(password);

            return password == GetPasswordByName(userName);
        }

        public static List<Webuser> GetAll()
        {
            var listWebusers = new List<Webuser>();

            using (var sqlConnection = new SqlConnection(Database.WebUiConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetAllWebusers";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();

                var sqlDr = sqlCommand.ExecuteReader();

                while (sqlDr.Read())
                {
                    var roleId = sqlDr.GetInt32(1);
                    var roleName = GetRoleNameByRoleId(roleId);
                    var role = Role.Create(roleName);

                    var name = sqlDr.GetString(2);
                    var password = sqlDr.GetString(3);

                    var hash = GetPasswordFromHash(password);

                    listWebusers.Add(new Webuser(name, role, hash));
                }
            }

            return listWebusers;
        }

        private static Webuser GetLoggedUser(string userName, string userPass)
        {
            NullCheck(userName);
            EmptyStringCheck(userName);

            NullCheck(userName);
            EmptyStringCheck(userPass);

            var roleId = GetRoleIdByUserName(userName);
            var roleName = GetRoleNameByRoleId(roleId);

            NullCheck(roleName);
            EmptyStringCheck(roleName);

            var userRole = Role.Get(roleName);

            NullCheck(userRole);

            var webuser = Create(userName, userRole, userPass);

            return webuser;
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

            return GetHashFromListPass(listPass);
        }

        private static string GetHashFromListPass(List<string> listPass)
        {
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
                var bytes = BitConverter.GetBytes(int.Parse(subHash));
                var subPass = Encoding.ASCII.GetString(bytes);

                passSB.Append(subPass);
            }

            return passSB.ToString().TrimEnd();
        }

        private static string GetPasswordByName(string userName)
        {
            NullCheck(userName);
            EmptyStringCheck(userName);

            var hash = GetPasswordHash(userName);
            var password = GetPasswordFromHash(hash);

            NullCheck(password);
            EmptyStringCheck(password);

            return password;
        }

        private static int GetRoleIdByUserName(string userName)
        {
            var roleId = -1;

            using (var sqlConnection = new SqlConnection(Database.WebUiConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetRoleIdByUserName";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParUserName(userName));
                sqlCommand.Parameters.Add(SqlParRoleId());

                sqlConnection.Open();

                roleId = GetRoleId(userName, roleId, sqlCommand);
            }

            return roleId;
        }

        internal static bool PermissionsEdit(string[] panelRoleNames)
        {
            NullCheck(panelRoleNames);



            return true;
        }

        private static int GetRoleId(string userName, int roleId, SqlCommand sqlCommand)
        {
            var sqlDr = sqlCommand.ExecuteReader();

            while (sqlDr.Read())
            {
                roleId = sqlDr.GetInt32(0);
            }

            if (roleId == -1)
            {
                throw new Exception($"Can't find role id by '{userName}' in database!");
            }

            return roleId;
        }

        private static string GetPasswordHash(string userName)
        {
            var hash = string.Empty;

            using (var sqlConnection = new SqlConnection(Database.WebUiConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetPasswordHashFormUserName";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParUserName(userName));
                sqlCommand.Parameters.Add(SqlParPasswordHash());

                sqlConnection.Open();

                hash = GetHash(hash, sqlCommand);
            }

            return hash;
        }

        private static string GetHash(string hash, SqlCommand sqlCommand)
        {
            var sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                hash = (string)sqlDataReader[0];
            }

            if (hash == string.Empty)
            {
                throw new Exception("Can't find password hash in database!");
            }

            return hash;
        }

        internal static bool Register(Webuser user)
        {
            NullCheck(user);

            if (Add(user))
            {
                Current = user;

                return true;
            }
            else
            {
                return false;
            }
        }

        internal static bool NameExists(string userName)
        {
            int userCount = 0;

            userCount = GetUserNameCount(userName, userCount);

            return userCount == 1;
        }

        private static string GetRoleNameByRoleId(int idRole)
        {
            var roleName = string.Empty;

            using (var sqlConnection = new SqlConnection(Database.WebUiConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetRoleNameByIdRole";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParIdRole(idRole));
                sqlCommand.Parameters.Add(SqlParRoleName());

                sqlConnection.Open();

                roleName = GetRoleName(idRole, roleName, sqlCommand);
            }

            return roleName;
        }

        private static string GetRoleName(int idRole, string roleName, SqlCommand sqlCommand)
        {
            var sqlDr = sqlCommand.ExecuteReader();

            while (sqlDr.Read())
            {
                roleName = sqlDr.GetString(0);
            }

            if (roleName == string.Empty)
            {
                throw new Exception($"Can't find role name by '{idRole}'!");
            }

            return roleName;
        }

        private static int GetUserNameCount(string userName, int userCount)
        {
            using (var sqlConnection = new SqlConnection(Database.WebUiConnectionString))
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

                userCount = GetCount(userCount, sqlCommand);
            }

            return userCount;
        }

        private static int GetCount(int userCount, SqlCommand sqlCommand)
        {
            var sqlDr = sqlCommand.ExecuteReader();

            while (sqlDr.Read())
            {
                userCount = sqlDr.GetInt32(0);
            }

            return userCount;
        }

        public static bool Add(Webuser webuser)
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
        }

        private static void AddWebuser(Webuser webuser)
        {
            var role = webuser?.Role;

            NullCheck(role);

            var roleId = GetRoleId(role);

            using (var sqlConnection = new SqlConnection(Database.WebUiConnectionString))
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

        private static int GetRoleId(Role role) => GetIdRoleByName(role.Name);

        private static int GetIdRoleByName(string roleName)
        {
            int IdRole = -1;

            using (var sqlConnection = new SqlConnection(Database.WebUiConnectionString))
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
            var sqlDr = sqlCommand.ExecuteReader();

            while (sqlDr.Read())
            {
                IdRole = sqlDr.GetInt32(0);
            }

            if (IdRole == -1)
            {
                throw new Exception($"Can't find id role by '{roleName}'!");
            }

            return IdRole;
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

        private static SqlParameter SqlParIdRole()
        {
            return new SqlParameter
            {
                ParameterName = "@IdRole",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
        }

        private static SqlParameter SqlParIdRole(int idRole)
        {
            return new SqlParameter
            {
                ParameterName = "@IdRole",
                SqlDbType = SqlDbType.Int,
                Value = idRole,
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

        private static SqlParameter SqlParRoleName()
        {
            return new SqlParameter
            {
                ParameterName = "@RoleName",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Output,
                Size = 50
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