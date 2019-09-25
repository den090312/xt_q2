using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace WEB_UI
{
    public static class Database
    {
        public static string WebUiConnectionString { get; }

        public static string WebUiScriptFileName { get; }

        static Database()
        {
            WebUiConnectionString = @"Data Source=DEN090312\SQLEXPRESS;Initial Catalog=webusersdb;Integrated Security=True";
            WebUiScriptFileName = "~/Scripts/webusersdb.sql";
        }

        public static void RunScript(HttpServerUtilityBase server, string path)
        {
            NullCheck(server);
            NullCheck(path);

            EmptyStringCheck(path);

            var scriptPath = server.MapPath(path);
            var scriptText = File.ReadAllText(scriptPath);

            var connectSql = new SqlConnection(WebUiConnectionString);
            var connectSrv = new ServerConnection(connectSql);

            new Server(connectSrv).ConnectionContext.ExecuteNonQuery(scriptText);
        }

        public static void RunScript(string connectionString, string scriptPath)
        {
            NullCheck(connectionString);
            EmptyStringCheck(connectionString);

            NullCheck(scriptPath);
            EmptyStringCheck(scriptPath);

            var scriptText = File.ReadAllText(scriptPath);
            NullCheck(scriptText);

            var connectSql = new SqlConnection(connectionString);
            NullCheck(connectSql);

            var connectSrv = new ServerConnection(connectSql);
            NullCheck(connectSrv);

            var server = new Server(connectSrv);
            NullCheck(server);

            server.ConnectionContext.ExecuteNonQuery(scriptText);
        }

        private static void EmptyStringCheck(string inputString)
        {
            if (inputString == string.Empty)
            {
                throw new Exception($"{nameof(inputString)} is empty!");
            }
        }

        private static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject == null)
            {
                throw new NullReferenceException($"{nameof(classObject)} is null!");
            }
        }
    }
}