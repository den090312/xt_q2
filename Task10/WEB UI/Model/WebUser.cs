﻿using System;
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
            Name = name;
            Role = role;
            PasswordHash = GetHashFromPassword(password);
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
                    ListWebusersAdd(ref listWebusers, sqlDr);
                }
            }

            return listWebusers;
        }

        public static void LogOut() => Current = Guest;

        public static bool LogIn(string name, string password)
        {
            NullCheck(name);
            EmptyStringCheck(name);

            NullCheck(password);
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

        public static Webuser Create(string name, Role role, string password)
        {
            NullCheck(name);
            EmptyStringCheck(name);

            NullCheck(role);

            NullCheck(password);
            EmptyStringCheck(password);

            var webuser = new Webuser(name, role, password);

            List.Add(webuser);

            Role.AddUserToRole(webuser, role);

            return webuser;
        }

        public static bool PasswordIsOk(string userName, string password)
        {
            NullCheck(userName);
            EmptyStringCheck(userName);

            NullCheck(password);
            EmptyStringCheck(password);

            return password == GetPasswordByName(userName);
        }

        public static bool RolesEdit(string[] roleNames)
        {
            NullCheck(roleNames);

            if (roleNames.Length == 0)
            {
                return false;
            }

            var webusers = GetAll();

            NullCheck(webusers);

            UpdateRoles(roleNames, webusers);

            return true;
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

        public static bool Register(Webuser user)
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

        public static bool NameExists(string userName)
        {
            NullCheck(userName);
            EmptyStringCheck(userName);

            return GetUserNameCount(userName) == 1;
        }

        private static void UpdateRoles(string[] roleNames, List<Webuser> webusers)
        {
            var i = 0;

            foreach (var webuser in webusers)
            {
                if (webuser.Role.Name != roleNames[i])
                {
                    var roleId = GetIdRoleByName(roleNames[i]);

                    UpdateWebuserRole(webuser.Name, roleId);
                }

                i++;
            }
        }

        private static void ListWebusersAdd(ref List<Webuser> listWebusers, SqlDataReader sqlDr)
        {
            var roleId = sqlDr.GetInt32(1);
            var roleName = GetRoleNameByRoleId(roleId);
            var role = Role.Create(roleName);

            var name = sqlDr.GetString(2);
            var password = sqlDr.GetString(3);

            var hash = GetPasswordFromHash(password);

            listWebusers.Add(new Webuser(name, role, hash));
        }

        private static Webuser GetLoggedUser(string userName, string userPass)
        {
            var roleId   = GetRoleIdByUserName(userName);
            var roleName = GetRoleNameByRoleId(roleId);
            var userRole = Role.Get(roleName);
            var webuser  = Create(userName, userRole, userPass);

            return webuser;
        }

        private static string GetHashFromPassword(string password)
        {
            var listPass = new List<string>();
            var step = 4;

            for (var i = 0; i < password.Length; i += step)
            {
                listPass.Add(password.Substring(i, Math.Min(step, password.Length - i)));
            }

            return GetHashFromListPass(listPass, step);
        }

        private static string GetHashFromListPass(List<string> listPass, int step)
        {
            var hashSb = new StringBuilder();

            for (var i = 0; i < listPass.Count; i++)
            {
                var bytes = Encoding.ASCII.GetBytes(AppendSpacesToString(listPass[i], step));

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

        private static string GetPasswordByName(string userName) => GetPasswordFromHash(GetPasswordHash(userName));

        private static int GetRoleIdByUserName(string userName)
        {
            using (var sqlConnection = new SqlConnection(Database.WebUiConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetRoleIdByUserName";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParUserName(userName));
                sqlCommand.Parameters.Add(SqlParRoleId());

                sqlConnection.Open();

                return GetRoleId(userName, sqlCommand.ExecuteReader());
            }
        }

        private static void UpdateWebuserRole(string newWebuserName, int rolId)
        {
            using (var sqlConnection = new SqlConnection(Database.WebUiConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "UpdateWebuserRole";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParRoleId(rolId));
                sqlCommand.Parameters.Add(SqlParName(newWebuserName));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }

        private static int GetRoleId(string userName, SqlDataReader sqlDr)
        {
            var roleId = -1;

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

                hash = GetHash(hash, sqlCommand.ExecuteReader());
            }

            return hash;
        }

        private static string GetHash(string hash, SqlDataReader sqlDr)
        {
            while (sqlDr.Read())
            {
                hash = sqlDr.GetString(0);
            }

            if (hash == string.Empty)
            {
                throw new Exception("Can't find password hash in database!");
            }

            return hash;
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

                roleName = GetRoleName(idRole, sqlCommand.ExecuteReader());
            }

            return roleName;
        }

        private static string GetRoleName(int idRole, SqlDataReader sqlDr)
        {
            var roleName = string.Empty;

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

        private static int GetUserNameCount(string userName)
        {
            using (var sqlConnection = new SqlConnection(Database.WebUiConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "UserNameCount";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParUserName(userName));
                sqlCommand.Parameters.Add(SqlParCount());

                sqlConnection.Open();

                return GetUserCount(sqlCommand.ExecuteReader());
            }
        }

        private static int GetUserCount(SqlDataReader sqlDr)
        {
            var userCount = 0;

            while (sqlDr.Read())
            {
                userCount = sqlDr.GetInt32(0);
            }

            return userCount;
        }

        private static void AddWebuser(Webuser webuser)
        {
            var roleId = GetRoleId(webuser.Role);

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
                IdRole = sqlDr.GetInt32(0);
            }

            if (IdRole == -1)
            {
                throw new Exception($"Can't find id role by '{roleName}'!");
            }

            return IdRole;
        }

        private static SqlParameter SqlParCount()
        {
            return new SqlParameter
            {
                ParameterName = "@Count",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
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

        private static SqlParameter SqlParName(string name)
        {
            return new SqlParameter
            {
                ParameterName = "@Name",
                Value = name,
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