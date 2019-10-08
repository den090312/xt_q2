﻿using Entities;
using InterfacesDAL;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class OrderProductDaoDb : IOrderProductDao, ILoggerDao
    {
        private static readonly string connectionString = @"Data Source=DEN090312\SQLEXPRESS;Initial Catalog=orderservice;Integrated Security=True";

        public ILog Log { get; } = LogManager.GetLogger(Logger.Name);

        public void StartLogger() => XmlConfigurator.Configure(Logger.Config);

        public bool Add(OrderProduct orderProduct)
        {
            try
            {
                AddOrderProduct(orderProduct);

                return true;
            }
            catch (Exception ex)
            {
                StartLogger();
                var exMessage = ex.Message.Replace(Environment.NewLine, "");
                Log.Error(exMessage + " Ошибка добавления товара в заказ, id товара: " + orderProduct.IdProduct + ", id заказа: " + orderProduct.IdOrder);

                return false;
            }
        }

        public IEnumerable<int> GetProductIds(int orderId)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetProductIdsByIdOrder";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParId(orderId));

                try
                {
                    sqlConnection.Open();

                    return ProductIds(sqlCommand);
                }
                catch (Exception ex)
                {
                    StartLogger();
                    var exMessage = ex.Message.Replace(Environment.NewLine, "");
                    Log.Error(exMessage + " Ошибка получения товаров по id заказа: " + orderId);

                    return new List<int>();
                }
            }
        }

        private static List<int> ProductIds(SqlCommand sqlCommand)
        {
            var sqlDr = sqlCommand.ExecuteReader();

            var productIds = new List<int>();

            while (sqlDr.Read())
            {
                productIds.Add(sqlDr.GetInt32(0));
            }

            return productIds;
        }

        private void AddOrderProduct(OrderProduct orderProduct)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "AddOrderProduct";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParIdOrder(orderProduct.IdOrder));
                sqlCommand.Parameters.Add(SqlParIdProduct(orderProduct.IdProduct));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
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

        private SqlParameter SqlParIdOrder(int idOrder)
        {
            return new SqlParameter
            {
                ParameterName = "@IdOrder",
                Value = idOrder,
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input
            };
        }

        private SqlParameter SqlParIdProduct(int idProduct)
        {
            return new SqlParameter
            {
                ParameterName = "@IdProduct",
                Value = idProduct,
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input
            };
        }
    }
}