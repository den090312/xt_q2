using Entities;
using InterfacesDAL;
using log4net;
using log4net.Config;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class FeedbackDaoDb : IFeedbackDao, ILoggerDao
    {
        private static readonly string connectionString = @"Data Source=DEN090312\SQLEXPRESS;Initial Catalog=orderservice;Integrated Security=True";

        public ILog Log { get; } = LogManager.GetLogger(Logger.Name);

        public void StartLogger() => XmlConfigurator.Configure(Logger.ConfigFile);

        public bool Add(string name, string text)
        {
            try
            {
                AddFeedback(name, text);

                return true;
            }
            catch (Exception ex)
            {
                StartLogger();
                var exMessage = ex.Message.Replace(Environment.NewLine, "");
                Log.Error(exMessage + $" Ошибка добавления отзыва, имя: '{name}', текст: '{text}'");

                return false;
            }
        }

        private void AddFeedback(string name, string text)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "AddFeedback";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParName(name));
                sqlCommand.Parameters.Add(SqlParText(text));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }

        private SqlParameter SqlParText(string text)
        {
            return new SqlParameter
            {
                ParameterName = "@Text",
                Value = text,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };
        }

        private SqlParameter SqlParName(string name)
        {
            return new SqlParameter
            {
                ParameterName = "@Name",
                Value = name,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };
        }
    }
}
