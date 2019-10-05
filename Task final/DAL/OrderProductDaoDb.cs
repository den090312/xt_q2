using Entities;
using InterfacesDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrderProductDaoDb : IOrderProductDao
    {
        private static readonly string connectionString = @"Data Source=DEN090312\SQLEXPRESS;Initial Catalog=orderservice;Integrated Security=True";

        public bool Add(OrderProduct orderProduct)
        {
            try
            {
                AddOrderProduct(orderProduct);

                return true;
            }
            catch (Exception ex)
            {
                LogOrderProductError(orderProduct, ex);

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
                    Logger.InitLogger();
                    Logger.Log.Error(ex.Message);

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

        private void LogOrderProductError(OrderProduct orderProduct, Exception ex)
        {
            var productInfo = "id заказа - " + orderProduct.IdOrder + ", id продукта - " + orderProduct.IdProduct;

            Logger.InitLogger();
            Logger.Log.Error(ex.Message + " - " + productInfo);
        }

        public IEnumerable<OrderProduct> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderProduct> GetByOrderId(int orderId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderProduct> GetByProductId(int productId)
        {
            throw new NotImplementedException();
        }

        public bool NoOrderProducts()
        {
            throw new NotImplementedException();
        }

        public bool RemoveByOrderId(int orderId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveByProductId(int productId)
        {
            throw new NotImplementedException();
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
