using Entities;
using InterfacesDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class AwardDaoDb : IAwardDao
    {
        private static readonly string connectionString = @"Data Source=DEN090312\SQLEXPRESS;Initial Catalog=webusersdb;Integrated Security=True";

        public bool Add(Award award)
        {
            try
            {
                AddAward(award);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void AddAward(Award award)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "AddAward";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParGuid(award.Guid));
                sqlCommand.Parameters.Add(SqlParTitle(award.Title));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }

        public bool RemoveByGuid(Guid awardGuid)
        {
            try
            {
                RemoveUserAward(awardGuid);
                RemoveAward(awardGuid);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void RemoveUserAward(Guid awardGuid)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "RemoveUserAwardByAwardGuid";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(SqlParAwardGuid(awardGuid));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }

        private void RemoveAward(Guid guid)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "RemoveAwardByGuid";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(SqlParGuid(guid));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }

        public IEnumerable<Award> GetAll()
        {
            var awards = new List<Award>();

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetAllAwards";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();

                var sqlDr = sqlCommand.ExecuteReader();

                while (sqlDr.Read())
                {
                    var guid = Guid.Parse(sqlDr["Guid"].ToString());
                    var title = sqlDr["Title"].ToString();

                    awards.Add(new Award(guid, title));
                }
            }

            return awards;
        }

        public Award GetByGuid(Guid guid)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetAwardByGuid";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(SqlParGuid(guid));
                sqlCommand.Parameters.Add(SqlParTitle());
                sqlConnection.Open();

                var sqlDr = sqlCommand.ExecuteReader();

                var name = string.Empty;

                while (sqlDr.Read())
                {
                    return new Award(guid, sqlDr.GetString(0));
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

                sqlCommand.CommandText = "PrintAwardsInfo";
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

        private static SqlParameter SqlParTitle(string title)
        {
            return new SqlParameter
            {
                ParameterName = "@Title",
                Value = title,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };
        }

        private static SqlParameter SqlParTitle()
        {
            return new SqlParameter
            {
                ParameterName = "@Title",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Output,
                Size = 30
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
    }
}
