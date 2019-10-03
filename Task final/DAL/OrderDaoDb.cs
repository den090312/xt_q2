using Entities;
using InterfacesDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class OrderDaoDb : IOrderDao
    {
        private static readonly string connectionString = @"Data Source=DEN090312\SQLEXPRESS;Initial Catalog=orderservice;Integrated Security=True";

        public bool Add(ref Order order)
        {
            try
            {
                AddOrder(ref order);

                return true;
            }
            catch (Exception ex)
            {
                LogOrderError(order, ex);

                return false;
            }
        }

        public IEnumerable<Order> GetByIdCustomer(int id)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetOrdersByIdCustomer";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParId(id));

                sqlConnection.Open();

                return GetOrdersById(sqlCommand);
            }
        }

        private IEnumerable<Order> GetOrdersById(SqlCommand sqlCommand)
        {
            var sqlDr = sqlCommand.ExecuteReader();

            var orders = new List<Order>();

            while (sqlDr.Read())
            {
                var id     = sqlDr.GetInt32(0);
                var date   = sqlDr.GetDateTime(1);
                var adress = sqlDr.GetString(2);
                var sum    = sqlDr.GetDecimal(3);
                var status = sqlDr.GetString(4);

                var currentStatus = (Order.Status)Enum.Parse(typeof(Order.Status), status);

                var order = new Order(id, date, adress, new List<int>(), sum, currentStatus);

                orders.Add(order);
            }

            return orders;
        }

        private void AddOrder(ref Order order)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "AddOrder";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParId(order.IdCustomer));
                sqlCommand.Parameters.Add(SqlParDate(order.Date));
                sqlCommand.Parameters.Add(SqlParAdress(order.Adress));
                sqlCommand.Parameters.Add(SqlParStatus(order.CurrentStatus));
                sqlCommand.Parameters.Add(SqlParSum(order.Sum));

                sqlConnection.Open();

                SetCustomerId(ref order, sqlCommand);
            }
        }

        private void SetCustomerId(ref Order order, SqlCommand sqlCommand)
        {
            var sqlDr = sqlCommand.ExecuteReader();

            while (sqlDr.Read())
            {
                order.Id = sqlDr.GetInt32(0);
            }
        }

        private static void LogOrderError(Order order, Exception ex)
        {
            var productInfo = "id заказа - " + order.Id;

            Logger.InitLogger();
            Logger.Log.Error(ex.Message + " - " + productInfo);
        }

        private SqlParameter SqlParSum(decimal sum)
        {
            return new SqlParameter
            {
                ParameterName = "@Sum",
                Value = sum,
                SqlDbType = SqlDbType.Decimal,
                Direction = ParameterDirection.Input
            };
        }

        private SqlParameter SqlParStatus(Order.Status currentStatus)
        {
            return new SqlParameter
            {
                ParameterName = "@Status",
                Value = Enum.GetName(typeof(Order.Status), currentStatus),
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

        private SqlParameter SqlParDate(DateTime date)
        {
            return new SqlParameter
            {
                ParameterName = "@Date",
                Value = date.ToString("yyyy-MM-dd"),
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };
        }

        private SqlParameter SqlParAdress(string adress)
        {
            return new SqlParameter
            {
                ParameterName = "@Adress",
                Value = adress,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };
        }
    }
}
