using Entities;
using InterfacesDAL;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
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

        public IEnumerable<Feedback> GetAll()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetAllFeedbacks";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                try
                {
                    sqlConnection.Open();

                    return GetAllFeedbacks(sqlCommand);
                }
                catch (Exception ex)
                {
                    StartLogger();
                    var exMessage = ex.Message.Replace(Environment.NewLine, "");
                    Log.Error(exMessage + " Ошибка получения всех отзывов");

                    return new List<Feedback>();
                }
            }
        }

        private IEnumerable<Feedback> GetAllFeedbacks(SqlCommand sqlCommand)
        {
            var feedbackList = new List<Feedback>();

            var sqlDr = sqlCommand.ExecuteReader();

            while (sqlDr.Read())
            {
                var id   = sqlDr.GetInt32(0);
                var date = sqlDr.GetDateTime(1);
                var name = sqlDr.GetString(2);
                var text = sqlDr.GetString(3);

                feedbackList.Add(new Feedback(id, name, date, text));
            }

            return feedbackList;
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
