using Entities;
using InterfacesDAL;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DAL
{
    public class ManagerDaoDb : IManagerDao, ILoggerDao
    {
        private readonly string connectionString = @"Data Source=DEN090312\SQLEXPRESS;Initial Catalog=orderservice;Integrated Security=True";

        private static readonly string loggerName;

        private readonly FileInfo loggerConfig = new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

        public ILog Log { get; } = LogManager.GetLogger(loggerName);

        public void StartLogger() => XmlConfigurator.Configure(loggerConfig);

        static ManagerDaoDb() => loggerName = "LOGGER";

        public bool Add(ref Manager manager)
        {
            try
            {
                AddManager(ref manager);

                return true;
            }
            catch (Exception ex)
            {
                StartLogger();
                var exMessage = ex.Message.Replace(Environment.NewLine, "");
                Log.Error(exMessage + $" Ошибка добавления менеджера, имя: '{manager.Name}'");

                return false;
            }
        }

        public Manager GetByIdUser(int id)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetManagerByIdUser";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParId(id));

                try
                {
                    sqlConnection.Open();

                    return GetManager(sqlCommand, id);
                }
                catch (Exception ex)
                {
                    StartLogger();
                    var exMessage = ex.Message.Replace(Environment.NewLine, "");
                    Log.Error(exMessage + $" Ошибка получения менеджера по id пользователя: '{id}'");

                    return null;
                }
            }
        }

        public IEnumerable<Manager> GetAll()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetAllManagers";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                try
                {
                    sqlConnection.Open();

                    return GetAllManagers(sqlCommand);
                }
                catch (Exception ex)
                {
                    StartLogger();
                    var exMessage = ex.Message.Replace(Environment.NewLine, "");
                    Log.Error(exMessage + " Ошибка получения всех менеджеров");

                    return new List<Manager>();
                }
            }
        }

        private IEnumerable<Manager> GetAllManagers(SqlCommand sqlCommand)
        {
            var managerList = new List<Manager>();

            var sqlDr = sqlCommand.ExecuteReader();

            while (sqlDr.Read())
            {
                var id     = sqlDr.GetInt32(0);
                var idUser = sqlDr.GetInt32(1);
                var name   = sqlDr.GetString(2);
                var rank   = sqlDr.GetString(3);

                var currentRank = (Manager.Rank)Enum.Parse(typeof(Manager.Rank), rank);

                var manager = new Manager(idUser, name, currentRank)
                {
                    Id = id
                };

                managerList.Add(manager);
            }

            return managerList;
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

            StartLogger();
            Log.Error($"Менеджер с idUser '{idUser}' не найден!");

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
    }
}