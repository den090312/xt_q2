using Entities;
using InterfacesDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class ProductDaoDb : IProductDao
    {
        private static readonly string connectionString = @"Data Source=DEN090312\SQLEXPRESS;Initial Catalog=orderservice;Integrated Security=True";

        public bool Add(ref Product product)
        {
            try
            {
                AddProduct(ref product);

                return true;
            }
            catch (Exception ex)
            {
                LogProductError(product, ex);

                return false;
            }
        }

        public bool NoProducts() => GetProductsCount() == 0;

        public IEnumerable<Product> GetAll()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetAllProducts";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();

                return GetAllProducts(sqlCommand);
            }
        }

        public Product GetById(int id)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetProductById";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParId(id));

                sqlConnection.Open();

                return GetProductById(sqlCommand, id);
            }
        }

        public bool Remove(int id)
        {
            try
            {
                RemoveProduct(id);

                return true;
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error(ex.Message + " - id: " + id);

                return false;
            }
        }

        public void RemoveProduct(int id)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "RemoveProduct";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParId(id));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }

        private Product GetProductById(SqlCommand sqlCommand, int id)
        {
            var sqlDr = sqlCommand.ExecuteReader();

            while (sqlDr.Read())
            {
                var name = sqlDr.GetString(0);
                var price = sqlDr.GetDecimal(1);

                return new Product(id, name, price);
            }

            return null;
        }

        private IEnumerable<Product> GetAllProducts(SqlCommand sqlCommand)
        {
            var productList = new List<Product>();

            var sqlDr = sqlCommand.ExecuteReader();

            while (sqlDr.Read())
            {
                var id = sqlDr.GetInt32(0);
                var name = sqlDr.GetString(1);
                var price = sqlDr.GetDecimal(2);

                var product = new Product(id, name, price);

                productList.Add(product);
            }

            return productList;
        }

        private void AddProduct(ref Product product)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "AddProduct";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParName(product.Name));
                sqlCommand.Parameters.Add(SqlParPrice(product.Price));

                sqlConnection.Open();

                SetProductId(ref product, sqlCommand);
            }
        }

        private static void SetProductId(ref Product product, SqlCommand sqlCommand)
        {
            var sqlDr = sqlCommand.ExecuteReader();

            while (sqlDr.Read())
            {
                product.Id = sqlDr.GetInt32(0);
            }
        }

        private static int GetProductsCount()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetProductsCount";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();

                return ProductsCount(sqlCommand);
            }
        }

        private static int ProductsCount(SqlCommand sqlCommand)
        {
            var count = 0;

            var sqlDr = sqlCommand.ExecuteReader();

            while (sqlDr.Read())
            {
                count = sqlDr.GetInt32(0);
            }

            return count;
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

        private SqlParameter SqlParPrice(decimal price)
        {
            return new SqlParameter
            {
                ParameterName = "@Price",
                Value = price,
                SqlDbType = SqlDbType.Decimal,
                Direction = ParameterDirection.Input
            };
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

        private static void LogProductError(Product product, Exception ex)
        {
            var productInfo = product.Id + " | " + product.Name + " | " + product.Price;

            Logger.InitLogger();
            Logger.Log.Error(ex.Message + " - " + productInfo);
        }
    }
}
