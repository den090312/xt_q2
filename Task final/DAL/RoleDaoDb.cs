using Entities;
using InterfacesDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class RoleDaoDb : IRoleDao
    {
        private static readonly string connectionString = @"Data Source=DEN090312\SQLEXPRESS;Initial Catalog=orderservice;Integrated Security=True";

        public bool Add(ref Role role)
        {
            try
            {
                AddRole(ref role);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Role> GetAll()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetAllRoles";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();

                return GetAllRoles(sqlCommand);
            }
        }

        public bool Remove(int roleId)
        {
            try
            {
                RemoveRole(roleId);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateName(ref Role role)
        {
            try
            {
                UpdateRoleName(ref role);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool NoRoles() => GetRoleCount() == 0;

        private static int GetRoleCount()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetRoleCount";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();

                return sqlCommand.ExecuteReader().GetInt32(0);
            }
        }

        private void AddRole(ref Role role)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "AddRole";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParName(role.Name));

                sqlCommand.Parameters.Add(SqlParBit(role.ProductRead,  "@ProductRead"));
                sqlCommand.Parameters.Add(SqlParBit(role.ProductWrite, "@ProductWrite"));
                sqlCommand.Parameters.Add(SqlParBit(role.OrderRead,    "@OrderRead"));
                sqlCommand.Parameters.Add(SqlParBit(role.OrderWrite,   "@OrderWrite"));
                sqlCommand.Parameters.Add(SqlParBit(role.RoleRead,     "@RoleRead"));
                sqlCommand.Parameters.Add(SqlParBit(role.RoleWrite,    "@RoleWrite"));
                sqlCommand.Parameters.Add(SqlParBit(role.UserRead,     "@UserRead"));
                sqlCommand.Parameters.Add(SqlParBit(role.UserWrite,    "@UserWrite"));

                sqlConnection.Open();

                role.Id = sqlCommand.ExecuteReader().GetInt32(0);
            }
        }

        private SqlParameter SqlParBit(bool value, string parameterName)
        {
            return new SqlParameter
            {
                ParameterName = parameterName,
                Value = value,
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.Input
            };
        }

        private void UpdateRoleName(ref Role role)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "UpdateRoleName";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParId(role.Id));
                sqlCommand.Parameters.Add(SqlParName(role.Name));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }

        private static List<Role> GetAllRoles(SqlCommand sqlCommand)
        {
            var roleList = new List<Role>();

            var sqlDr = sqlCommand.ExecuteReader();

            while (sqlDr.Read())
            {
                var id = sqlDr.GetInt32(0);
                var name = sqlDr.GetString(1);

                var role = new Role(id, name)
                {
                    ProductRead  = sqlDr.GetBoolean(2),
                    ProductWrite = sqlDr.GetBoolean(3),
                    OrderRead    = sqlDr.GetBoolean(4),
                    OrderWrite   = sqlDr.GetBoolean(5),
                    RoleRead     = sqlDr.GetBoolean(6),
                    RoleWrite    = sqlDr.GetBoolean(7),
                    UserRead     = sqlDr.GetBoolean(8),
                    UserWrite    = sqlDr.GetBoolean(9)
                };

                roleList.Add(role);
            }

            return roleList;
        }

        private void RemoveRole(int roleId)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "RemoveRole";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(SqlParId(roleId));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
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
    }
}