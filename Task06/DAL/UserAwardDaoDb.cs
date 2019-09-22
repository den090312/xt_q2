using Entities;
using InterfacesDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class UserAwardDaoDb : IUserAwardDao
    {
        private static readonly string connectionString = @"Data Source=DEN090312\SQLEXPRESS;Initial Catalog=webusersdb;Integrated Security=True";

        public bool JoinAwardToUser(User user, Award award)
        {
            try
            {
                Join(user, award);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Join(User user, Award award)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "JoinAwardToUser";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(SqlParGuid(user.Guid));
                sqlCommand.Parameters.Add(SqlParGuid(award.Guid));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }

        public IEnumerable<Award> GetAwardsByUser(User user, IEnumerable<Award> awards)
        {
            throw new NotImplementedException();
        }

        public string GetInfo()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "PrintUsersAwardsInfo";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();

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

        public bool RemoveUserAwards(Guid userGuid, IEnumerable<User> users, IEnumerable<Award> awards)
        {
            throw new NotImplementedException();
        }

        public bool RemoveAwardUsers(Guid awardGuid, IEnumerable<User> users, IEnumerable<Award> awards)
        {
            throw new NotImplementedException();
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
