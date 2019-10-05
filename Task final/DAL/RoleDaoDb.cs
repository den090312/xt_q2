﻿using Entities;
using InterfacesDAL;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DAL
{
    public class RoleDaoDb : IRoleDao, ILoggerDao
    {
        private static readonly string connectionString = @"Data Source=DEN090312\SQLEXPRESS;Initial Catalog=orderservice;Integrated Security=True";

        public ILog Log { get; } = LogManager.GetLogger("LOGGER");

        public bool Add(ref Role role)
        {
            try
            {
                AddRole(ref role);

                return true;
            }
            catch (Exception ex)
            {
                LogRoleError(role, ex);

                return false;
            }
        }

        public bool Remove(int roleId)
        {
            try
            {
                RemoveRole(roleId);

                return true;
            }
            catch (Exception ex)
            {
                InitLogger();
                Log.Error(ex.Message + " - roleId: " + roleId);

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
            catch (Exception ex)
            {
                LogRoleError(role, ex);

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

                try
                {
                    sqlConnection.Open();

                    return GetAllRoles(sqlCommand);
                }
                catch (Exception ex)
                {
                    InitLogger();
                    Log.Error(ex.Message);

                    return new List<Role>();
                }
            }
        }

        public bool NoRoles() => GetRolesCount() == 0;

        private int GetRolesCount()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetRolesCount";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                try
                {
                    sqlConnection.Open();

                    return GetCount(sqlCommand);
                }
                catch (Exception ex)
                {
                    InitLogger();
                    Log.Error(ex.Message);

                    return 0;
                }
            }
        }

        public Role GetById(int id)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetRoleById";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParId(id));

                try
                {
                    sqlConnection.Open();

                    return GetRoleById(sqlCommand, id);
                }
                catch (Exception ex)
                {
                    InitLogger();
                    Log.Error(ex.Message);

                    return null;
                }
            }
        }

        public int GetIdByName(string name)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetRoleIdByName";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParName(name));
                sqlCommand.Parameters.Add(SqlParId());

                try
                {
                    sqlConnection.Open();

                    return GetRoleId(sqlCommand);
                }
                catch (Exception ex)
                {
                    InitLogger();
                    Log.Error(ex.Message);

                    return 0;
                }
            }
        }

        private int GetRoleId(SqlCommand sqlCommand)
        {
            var roleId = 0;

            var sqlDr = sqlCommand.ExecuteReader();

            while (sqlDr.Read())
            {
                roleId = sqlDr.GetInt32(0);
            }

            return roleId;
        }

        private Role GetRoleById(SqlCommand sqlCommand, int id)
        {
            var sqlDr = sqlCommand.ExecuteReader();

            while (sqlDr.Read())
            {
                var role = new Role(id, sqlDr.GetString(1))
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

                return role;
            }

            return null;
        }

        private int GetCount(SqlCommand sqlCommand)
        {
            var count = 0;

            var sqlDr = sqlCommand.ExecuteReader();

            while (sqlDr.Read())
            {
                count = sqlDr.GetInt32(0);
            }

            return count;
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

                SetRoleId(ref role, sqlCommand);
            }
        }

        private static void SetRoleId(ref Role role, SqlCommand sqlCommand)
        {
            var sqlDr = sqlCommand.ExecuteReader();

            while (sqlDr.Read())
            {
                role.Id = sqlDr.GetInt32(0);
            }
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

        private static SqlParameter SqlParName()
        {
            return new SqlParameter
            {
                ParameterName = "@Name",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Output,
                Size = 15
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

        private SqlParameter SqlParId()
        {
            return new SqlParameter
            {
                ParameterName = "@Id",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
        }

        private void LogRoleError(Role role, Exception ex)
        {
            var roleInfo = role.Id + " | " + role.Name;

            InitLogger();
            Log.Error(ex.Message + " - " + roleInfo);
        }

        public void InitLogger()
        {
            var configFile = new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            XmlConfigurator.Configure(configFile);
        }
    }
}