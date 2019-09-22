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
        private static readonly string connectionString = @"Data Source=DEN090312\SQLEXPRESS;Initial Catalog=webusersdb;Integrated Security=True";

        private static readonly string sqlDateFormat = "yyyy-MM-dd";

        public bool Add(User user)
        {
            try
            {
                AddUser(user);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveByGuid(Guid userGuid)
        {
            try
            {
                RemoveUser(userGuid);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void AddUser(User user)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "AddUser";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParGuid(user.Guid));
                sqlCommand.Parameters.Add(SqlParName(user.Name));
                sqlCommand.Parameters.Add(SqlParDate(user.DateOfBirth.ToString(sqlDateFormat)));
                sqlCommand.Parameters.Add(SqlParAge(user.Age));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }

        private void RemoveUser(Guid guid)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "RemoveUserByGuid";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(SqlParGuid(guid));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }

        public IEnumerable<User> GetAll()
        {
            var users = new List<User>();

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetAllUsers";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();

                var sqlDr = sqlCommand.ExecuteReader();

                while (sqlDr.Read())
                {
                    var guid = Guid.Parse(sqlDr["Guid"].ToString());
                    var name = sqlDr["Name"].ToString();
                    var dateOfBirth = (DateTime)sqlDr["DateOfBirth"];

                    users.Add(new User(guid, name, dateOfBirth));
                }
            }

            return users;
        }

        public User GetByGuid(Guid guid)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetUserByGuid";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(SqlParGuid(guid));
                sqlCommand.Parameters.Add(SqlParName());
                sqlCommand.Parameters.Add(SqlParDate());
                sqlConnection.Open();

                var sqlDr = sqlCommand.ExecuteReader();

                var name = string.Empty;

                while (sqlDr.Read())
                {
                    return new User(guid, sqlDr.GetString(0), DateTime.Parse(sqlDr.GetString(1)));
                }

                if (name == string.Empty)
                {
                    return null;
                }
            }

            return null;
        }

        public string GetInfo()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "PrintUsersInfo";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();

                var sqlDr = sqlCommand.ExecuteReader();

                string info = string.Empty;
                var i = 0;

                while (sqlDr.Read())
                {
                    StringInfo(sqlDr, ref info, ref i);
                }

                return info.TrimEnd();
            }
        }

        private static void StringInfo(SqlDataReader sqlDr, ref string info, ref int i)
        {
            for (var j = 0; j < 4; j++)
            {
                info += sqlDr.GetString(j) + " ";
                i++;
            }
        }

        private static SqlParameter SqlParAge(int age)
        {
            return new SqlParameter
            {
                ParameterName = "@Age",
                Value = age,
                SqlDbType = SqlDbType.TinyInt,
                Direction = ParameterDirection.Input
            };
        }

        private static SqlParameter SqlParDate(string dateString)
        {
            return new SqlParameter
            {
                ParameterName = "@DateOfBirth",
                Value = dateString,
                SqlDbType = SqlDbType.Date,
                Direction = ParameterDirection.Input
            };
        }

        private static SqlParameter SqlParDate()
        {
            return new SqlParameter
            {
                ParameterName = "@DateOfBirth",
                SqlDbType = SqlDbType.Date,
                Direction = ParameterDirection.Output
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

        private static SqlParameter SqlParName()
        {
            return new SqlParameter
            {
                ParameterName = "@Name",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Output
            };
        }

        private static SqlParameter SqlParGuid(Guid guid)
        {
            return new SqlParameter
            {
                ParameterName = "@Guid",
                Value = guid,
                SqlDbType = SqlDbType.UniqueIdentifier,
                Direction = ParameterDirection.Input
            };
        }
    }
}
