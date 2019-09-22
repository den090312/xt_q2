using Entities;
using InterfacesDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDaoDb : IUserDao
    {
        private static readonly string connectionString = @"Data Source=DEN090312\SQLEXPRESS;Initial Catalog=webusersdb;Integrated Security=True";

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

        private static void AddUser(User user)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "AddUser";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParGuid(user));
                sqlCommand.Parameters.Add(SqlParName(user));
                sqlCommand.Parameters.Add(SqlParDate(user));
                sqlCommand.Parameters.Add(SqlParAge(user));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }

        private static SqlParameter SqlParAge(User user)
        {
            return new SqlParameter
            {
                ParameterName = "@Age",
                Value = user.Age,
                SqlDbType = SqlDbType.TinyInt,
                Direction = ParameterDirection.Input
            };
        }

        private static SqlParameter SqlParDate(User user)
        {
            return new SqlParameter
            {
                ParameterName = "@DateOfBirth",
                Value = user.DateOfBirth.ToString("yyyy-MM-dd"),
                SqlDbType = SqlDbType.Date,
                Direction = ParameterDirection.Input
            };
        }

        private static SqlParameter SqlParName(User user)
        {
            return new SqlParameter
            {
                ParameterName = "@Name",
                Value = user.Name,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };
        }

        private static SqlParameter SqlParGuid(User user)
        {
            return new SqlParameter
            {
                ParameterName = "@Guid",
                Value = user.Guid,
                SqlDbType = SqlDbType.UniqueIdentifier,
                Direction = ParameterDirection.Input
            };
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

                sqlCommand.ExecuteNonQuery();

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

        public User GetByGuid(Guid userGuid)
        {
            throw new NotImplementedException();
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
                    for (var j = 0; j < 4; j++)
                    {
                        info += sqlDr.GetString(j) + " ";
                        i++;
                    }
                }

                return info.TrimEnd();
            }
        }

        public bool RemoveByGuid(Guid userGuid)
        {
            throw new NotImplementedException();
        }
    }
}
