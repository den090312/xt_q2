﻿using Entities;
using InterfacesDAL;
using log4net;
using log4net.Config;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class CustomerDaoDb : ICustomerDao, ILoggerDao
    {
        private static readonly string connectionString = @"Data Source=DEN090312\SQLEXPRESS;Initial Catalog=orderservice;Integrated Security=True";

        public ILog Log { get; } = LogManager.GetLogger(Logger.Name);

        public void StartLogger() => XmlConfigurator.Configure(Logger.Config);

        public bool Add(ref Customer customer)
        {
            try
            {
                AddCustomer(ref customer);

                return true;
            }
            catch (Exception ex)
            {
                StartLogger();
                var exMessage = ex.Message.Replace(Environment.NewLine, "");
                Log.Error(exMessage + $" Ошибка добавления покупателя, имя: '{customer.Name}'");

                return false;
            }
        }

        public Customer GetByIdUser(int id)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetCustomerByIdUser";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParId(id));

                try
                {
                    sqlConnection.Open();

                    return GetCustomer(sqlCommand, id);
                }
                catch (Exception ex)
                {
                    StartLogger();
                    var exMessage = ex.Message.Replace(Environment.NewLine, "");
                    Log.Error(exMessage + $" Ошибка получения покупателя по id пользователя: {id}");

                    return null;
                }
            }
        }

        private Customer GetCustomer(SqlCommand sqlCommand, int idUser)
        {
            var sqlDr = sqlCommand.ExecuteReader();

            while (sqlDr.Read())
            {
                var id = sqlDr.GetInt32(0);
                var name = sqlDr.GetString(1);

                return new Customer(id, idUser, name);
            }

            StartLogger();
            Log.Error($"Покупатель с idUser = '{idUser}' не найден!");

            return null;
        }

        private void AddCustomer(ref Customer customer)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "AddCustomer";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParId(customer.IdUser));
                sqlCommand.Parameters.Add(SqlParName(customer.Name));

                sqlConnection.Open();

                SetCustomerId(ref customer, sqlCommand);
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

        private void SetCustomerId(ref Customer customer, SqlCommand sqlCommand)
        {
            var sqlDr = sqlCommand.ExecuteReader();

            while (sqlDr.Read())
            {
                customer.Id = sqlDr.GetInt32(0);
            }
        }
    }
}