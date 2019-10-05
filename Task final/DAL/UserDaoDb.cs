using Entities;
using InterfacesDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class UserDaoDb : IUserDao
    {
        private static readonly string connectionString = @"Data Source=DEN090312\SQLEXPRESS;Initial Catalog=orderservice;Integrated Security=True";

        public bool Add(ref User user)
        {
            try
            {
                AddUser(ref user);

                return true;
            }
            catch (Exception ex)
            {
                LogUserError(user, ex);

                return false;
            }
        }

        public bool Remove(int userId)
        {
            try
            {
                RemoveUser(userId);

                return true;
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error(ex.Message + " - userId: " + userId);

                return false;
            }
        }

        public bool UpdateName(User user)
        {
            try
            {
                UpdateUserName(user);

                return true;
            }
            catch (Exception ex)
            {
                LogUserError(user, ex);

                return false;
            }
        }

        public User GetByName(string name)
        {
            try
            {
                return GetUserByName(name);
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error(ex.Message + " - name: " + name);

                return null;
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetAllUsers";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                try
                {
                    sqlConnection.Open();

                    return GetAllUsers(sqlCommand);
                }
                catch (Exception ex)
                {
                    Logger.InitLogger();
                    Logger.Log.Error(ex.Message);

                    return new List<User>();
                }
            }
        }

        private User GetUserByName(string name)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetUserByName";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(SqlParName(name));

                sqlConnection.Open();

                return GetUser(sqlCommand, name);
            }
        }

        private User GetUser(SqlCommand sqlCommand, string name)
        {
            var sqlDr = sqlCommand.ExecuteReader();

            while (sqlDr.Read())
            {
                var id = sqlDr.GetInt32(0);
                var roleId = sqlDr.GetInt32(1);
                var passwordHash = sqlDr.GetString(2);

                return new User(id, roleId, name, passwordHash);
            }

            return null;
        }

        private void UpdateUserName(User user)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "UpdateRoleName";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParId(user.Id));
                sqlCommand.Parameters.Add(SqlParName(user.Name));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }

        private void RemoveUser(int userId)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "RemoveUser";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(SqlParId(userId));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }

        private IEnumerable<User> GetAllUsers(SqlCommand sqlCommand)
        {
            var userList = new List<User>();

            var sqlDr = sqlCommand.ExecuteReader();

            while (sqlDr.Read())
            {
                var id       = sqlDr.GetInt32(0);
                var roleId   = sqlDr.GetInt32(1);
                var name     = sqlDr.GetString(2);
                var passHash = sqlDr.GetString(3);

                var user = new User(id, roleId, name, passHash);

                userList.Add(user);
            }

            return userList;
        }

        private void AddUser(ref User user)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "AddUser";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParIdRole(user.IdRole));
                sqlCommand.Parameters.Add(SqlParName(user.Name));
                sqlCommand.Parameters.Add(SqlParPasswordHash(user.PasswordHash));

                sqlConnection.Open();

                SetUserId(ref user, sqlCommand);
            }
        }

        private static void SetUserId(ref User user, SqlCommand sqlCommand)
        {
            var sqlDr = sqlCommand.ExecuteReader();

            while (sqlDr.Read())
            {
                user.Id = sqlDr.GetInt32(0);
            }
        }

        private SqlParameter SqlParId(int id)
        {
            return new SqlParameter
            {
                ParameterName = "@Id",
                Value = id,
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input
            };
        }

        private SqlParameter SqlParPasswordHash(string passwordHash)
        {
            return new SqlParameter
            {
                ParameterName = "@PasswordHash",
                Value = passwordHash,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };
        }

        private SqlParameter SqlParIdRole(int idRole)
        {
            return new SqlParameter
            {
                ParameterName = "@IdRole",
                Value = idRole,
                SqlDbType = SqlDbType.Int,
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

        private static void LogUserError(User user, Exception ex)
        {
            var userInfo = user.Id + " | " + user.Name;

            Logger.InitLogger();
            Logger.Log.Error(ex.Message + " - " + userInfo);
        }
    }
}
