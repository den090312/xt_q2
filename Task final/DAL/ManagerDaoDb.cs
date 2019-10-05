using Entities;
using InterfacesDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class ManagerDaoDb : IManagerDao
    {
        private static readonly string connectionString = @"Data Source=DEN090312\SQLEXPRESS;Initial Catalog=orderservice;Integrated Security=True";

        public bool Add(ref Manager manager)
        {
            try
            {
                AddManager(ref manager);

                return true;
            }
            catch (Exception ex)
            {
                LogManagerError(manager, ex);

                return false;
            }
        }

        public Manager GetByIdUser(int idUser)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetManagerByIdUser";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(SqlParId(idUser));

                try
                {
                    sqlConnection.Open();

                    return GetManager(sqlCommand, idUser);
                }
                catch (Exception ex)
                {
                    Logger.InitLogger();
                    Logger.Log.Error(ex.Message);

                    return null;
                }
            }
        }

        public bool IsManager(int idUser)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetManagerCountByIdUser";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParId(idUser));

                try
                {
                    sqlConnection.Open();

                    return GetManagerCount(sqlCommand);
                }
                catch (Exception ex)
                {
                    Logger.InitLogger();
                    Logger.Log.Error(ex.Message);

                    return false;
                }
            }
        }

        private static bool GetManagerCount(SqlCommand sqlCommand)
        {
            var sqlDr = sqlCommand.ExecuteReader();

            while (sqlDr.Read())
            {
                return sqlDr.GetInt32(0) > 0;
            }

            return false;
        }

        private Manager GetManager(SqlCommand sqlCommand, int idUser)
        {
            var sqlDr = sqlCommand.ExecuteReader();

            while (sqlDr.Read())
            {
                var id = sqlDr.GetInt32(0);
                var name = sqlDr.GetString(1);
                var rank = sqlDr.GetString(2);

                var currentRank = (Manager.Rank)Enum.Parse(typeof(Manager.Rank), rank);

                var manager = new Manager(idUser, name, currentRank)
                {
                    Id = id
                };

                return manager;
            }

            return null;
        }

        private void AddManager(ref Manager manager)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "AddManager";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParId(manager.IdUser));
                sqlCommand.Parameters.Add(SqlParName(manager.Name));
                sqlCommand.Parameters.Add(SqlParRank(manager.CurrentRank));

                sqlConnection.Open();

                SetManagerId(ref manager, sqlCommand);
            }
        }

        private void SetManagerId(ref Manager manager, SqlCommand sqlCommand)
        {
            var sqlDr = sqlCommand.ExecuteReader();

            while (sqlDr.Read())
            {
                manager.Id = sqlDr.GetInt32(0);
            }
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

        private SqlParameter SqlParRank(Manager.Rank currentRank)
        {
            return new SqlParameter
            {
                ParameterName = "@Rank",
                Value = Enum.GetName(typeof(Manager.Rank), currentRank),
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };
        }

        private static void LogManagerError(Manager manager, Exception ex)
        {
            var managerInfo = manager.Id + " | " + manager.Name + " | ";

            Logger.InitLogger();
            Logger.Log.Error(ex.Message + " - " + managerInfo);
        }
    }
}
