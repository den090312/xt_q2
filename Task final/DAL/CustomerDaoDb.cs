using Entities;
using InterfacesDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class CustomerDaoDb : ICustomerDao
    {
        private static readonly string connectionString = @"Data Source=DEN090312\SQLEXPRESS;Initial Catalog=orderservice;Integrated Security=True";

        public bool Add(ref Customer customer)
        {
            try
            {
                AddCustomer(ref customer);

                return true;
            }
            catch (Exception ex)
            {
                LogCustomerError(customer, ex);

                return false;
            }
        }

        public Customer GetByUserId(int idUser)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetCustomerByIdUser";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(SqlParId(idUser));

                sqlConnection.Open();

                return GetCustomer(sqlCommand, idUser);
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

            return null;
        }

        public bool AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public bool ChangeName(string newName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrders()
        {
            throw new NotImplementedException();
        }

        public bool Remove(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void RemoveOrder(Order order)
        {
            throw new NotImplementedException();
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

        private static void LogCustomerError(Customer customer, Exception ex)
        {
            var productInfo = customer.Id + " | " + customer.Name;

            Logger.InitLogger();
            Logger.Log.Error(ex.Message + " - " + productInfo);
        }
    }
}
