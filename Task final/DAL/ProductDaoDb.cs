using Entities;
using InterfacesDAL;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class ProductDaoDb : IProductDao
    {
        private static readonly string connectionString = @"Data Source=DEN090312\SQLEXPRESS;Initial Catalog=orderservice;Integrated Security=True";

        public bool NoProducts() => GetProductsCount() == 0;

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
