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
    public class OrderDaoDb : IOrderDao, ILoggerDao
    {
        private static readonly string connectionString = @"Data Source=DEN090312\SQLEXPRESS;Initial Catalog=orderservice;Integrated Security=True";

        public ILog Log { get; } = LogManager.GetLogger(Logger.Name);

        public void StartLogger() => XmlConfigurator.Configure(Logger.Config);

        public bool Add(ref Order order)
        {
            try
            {
                AddOrder(ref order);

                return true;
            }
            catch (Exception ex)
            {
                StartLogger();
                var exMessage = ex.Message.Replace(Environment.NewLine, "");
                Log.Error(exMessage + " Ошибка добавления заказа, id: " + order.IdCustomer 
                    + ", дата заказа: " + order.Date.ToString("dd.MM.yyyy") 
                    + ", сумма: " + order.Sum);

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

                try
                {
                    sqlConnection.Open();

                    return GetOrdersByIdCustomer(sqlCommand, id);
                }
                catch (Exception ex)
                {
                    StartLogger();
                    var exMessage = ex.Message.Replace(Environment.NewLine, "");
                    Log.Error(exMessage + " Ошибка получения заказа по id покупателя, id: " + id);

                    return new List<Order>();
                }
            }
        }

        public IEnumerable<Order> GetByIdManager(int id)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetOrdersByIdManager";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParId(id));

                try
                {
                    sqlConnection.Open();

                    return GetOrdersByIdManager(sqlCommand, id);
                }
                catch (Exception ex)
                {
                    StartLogger();
                    var exMessage = ex.Message.Replace(Environment.NewLine, "");
                    Log.Error(exMessage + " Ошибка получения заказа по id менеджера, id: " + id);

                    return new List<Order>();
                }
            }
        }

        public bool CancelOrder(int id)
        {
            try
            {
                OrderCancel(id);

                return true;
            }
            catch (Exception ex)
            {
                StartLogger();
                var exMessage = ex.Message.Replace(Environment.NewLine, "");
                Log.Error(exMessage + " Ошибка отмены заказа, id: " + id);

                return false;
            }
        }

        public bool RestoreOrder(int id)
        {
            try
            {
                OrderRestore(id);

                return true;
            }
            catch (Exception ex)
            {
                StartLogger();
                var exMessage = ex.Message.Replace(Environment.NewLine, "");
                Log.Error(exMessage + " Ошибка восстановления заказа, id: " + id);

                return false;
            }
        }

        public bool InWorkOrder(int idOrder, int idManager)
        {
            try
            {
                OrderInWork(idOrder, idManager);

                return true;
            }
            catch (Exception ex)
            {
                StartLogger();
                var exMessage = ex.Message.Replace(Environment.NewLine, "");
                Log.Error(exMessage + " Ошибка взятия заказа в работу, id заказа: " + idOrder + ", id менеджера: " + idManager);

                return false;
            }
        }

        public bool CompleteOrder(int id)
        {
            try
            {
                OrderComplete(id);

                return true;
            }
            catch (Exception ex)
            {
                StartLogger();
                var exMessage = ex.Message.Replace(Environment.NewLine, "");
                Log.Error(exMessage + " Ошибка завершения заказа, id: " + id);

                return false;
            }
        }

        public IEnumerable<Order> GetNewOrders()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetNewOrders";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                try
                {
                    sqlConnection.Open();

                    return NewOrders(sqlCommand);
                }
                catch (Exception ex)
                {
                    StartLogger();
                    var exMessage = ex.Message.Replace(Environment.NewLine, "");
                    Log.Error(exMessage + " Ошибка получения новых заказов");

                    return new List<Order>();
                }
            }
        }

        private IEnumerable<Order> NewOrders(SqlCommand sqlCommand)
        {
            var sqlDr = sqlCommand.ExecuteReader();

            var orderList = new List<Order>();

            while (sqlDr.Read())
            {
                var orderId    = sqlDr.GetInt32(0);
                var idCustomer = sqlDr.GetInt32(1);
                var date       = sqlDr.GetDateTime(2);
                var adress     = sqlDr.GetString(3);
                var sum        = sqlDr.GetDecimal(4);

                var order = new Order(idCustomer, date, adress, new List<int>(), sum)
                {
                    Id = orderId
                };

                orderList.Add(order);
            }

            return orderList;
        }

        private void OrderInWork(int orderId, int idManager)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "InWorkOrder";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParId(orderId));
                sqlCommand.Parameters.Add(SqlParIdManager(idManager));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }

        private void OrderComplete(int id)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "CompleteOrderById";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParId(id));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }

        private void OrderRestore(int id)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "RestoreOrderById";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParId(id));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }

        private void OrderCancel(int id)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "CancelOrderById";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParId(id));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }

        private IEnumerable<Order> GetOrdersByIdCustomer(SqlCommand sqlCommand, int idCustomer)
        {
            var sqlDr = sqlCommand.ExecuteReader();

            var orders = new List<Order>();

            while (sqlDr.Read())
            {
                var orderId = sqlDr.GetInt32(0);
                var date    = sqlDr.GetDateTime(1);
                var adress  = sqlDr.GetString(2);
                var sum     = sqlDr.GetDecimal(3);
                var status  = sqlDr.GetString(4);

                var currentStatus = (Order.Status)Enum.Parse(typeof(Order.Status), status);

                var order = new Order(idCustomer, date, adress, new List<int>(), sum, currentStatus)
                {
                    Id = orderId
                };

                orders.Add(order);
            }

            return orders;
        }

        private IEnumerable<Order> GetOrdersByIdManager(SqlCommand sqlCommand, int idManager)
        {
            var sqlDr = sqlCommand.ExecuteReader();

            var orders = new List<Order>();

            while (sqlDr.Read())
            {
                var orderId    = sqlDr.GetInt32(0);
                var idCustomer = sqlDr.GetInt32(1);
                var date       = sqlDr.GetDateTime(2);
                var adress     = sqlDr.GetString(3);
                var sum        = sqlDr.GetDecimal(4);
                var status     = sqlDr.GetString(5);

                var currentStatus = (Order.Status)Enum.Parse(typeof(Order.Status), status);

                var order = new Order(idCustomer, date, adress, new List<int>(), sum, currentStatus)
                {
                    Id = orderId,
                    IdManager = idManager
                };

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

        private SqlParameter SqlParIdManager(int idManager)
        {
            return new SqlParameter
            {
                ParameterName = "@IdManager",
                Value = idManager,
                SqlDbType = SqlDbType.Int,
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