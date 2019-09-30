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
            catch
            {
                return false;
            }
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

                product.Id = sqlCommand.ExecuteReader().GetInt32(0);
            }
        }

        public bool NoProducts() => GetProductsCount() == 0;

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

        private static int GetProductsCount()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetProductsCount";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();

                return sqlCommand.ExecuteReader().GetInt32(0);
            }
        }
    }
}
