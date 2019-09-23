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

        public IEnumerable<Award> GetAwardsByUserGuid(Guid userGuid, IEnumerable<Award> awards)
        {
            var awardsByUser = new List<Award>();
            var awardGuids = new List<Guid>();

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetAwardGuidsByUserGuid";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(SqlParUserGuid(userGuid));
                sqlCommand.Parameters.Add(SqlParAwardGuid());

                sqlConnection.Open();

                var sqlDr = sqlCommand.ExecuteReader();

                while (sqlDr.Read())
                {
                    awardGuids.Add(sqlDr.GetGuid(0));
                }
            }

            foreach (var award in awards)
            {
                AddAwardByUser(ref awardsByUser, awardGuids, award);
            }

            return awardsByUser;
        }

        private static void AddAwardByUser(ref List<Award> awardsByUser, List<Guid> awardGuids, Award award)
        {
            foreach (var guid in awardGuids)
            {
                if (award.Guid == guid)
                {
                    awardsByUser.Add(award);
                }
            }
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
            try
            {
                UserAwardsRemove(userGuid);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void UserAwardsRemove(Guid userGuid)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "DeleteUserAwardByUserGuid";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(SqlParUserGuid(userGuid));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }

        private static void AddUserAward(ref List<UserAward> userAwards, User user, Award award)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetUserAwardCount";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(SqlParUserGuid(user.Guid));
                sqlCommand.Parameters.Add(SqlParAwardGuid(award.Guid));

                sqlConnection.Open();

                var sqlDr = sqlCommand.ExecuteReader();

                var counter = 0;

                while (sqlDr.Read())
                {
                    counter++;
                }

                if (counter > 0)
                {
                    userAwards.Add(new UserAward(user, award));
                }
            }
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

        private static SqlParameter SqlParUserGuid(Guid userGuid)
        {
            return new SqlParameter
            {
                ParameterName = "@UserGuid",
                Value = userGuid,
                SqlDbType = SqlDbType.UniqueIdentifier,
                Direction = ParameterDirection.Input
            };
        }

        private static SqlParameter SqlParAwardGuid(Guid awardGuid)
        {
            return new SqlParameter
            {
                ParameterName = "@AwardGuid",
                Value = awardGuid,
                SqlDbType = SqlDbType.UniqueIdentifier,
                Direction = ParameterDirection.Input
            };
        }

        private static SqlParameter SqlParAwardGuid()
        {
            return new SqlParameter
            {
                ParameterName = "@AwardGuid",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Direction = ParameterDirection.Output
            };
        }
    }
}
