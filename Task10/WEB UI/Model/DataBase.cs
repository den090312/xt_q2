using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
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
            var scriptPath = server.MapPath(path);
            var scriptText = File.ReadAllText(scriptPath);

            var connectSql = new SqlConnection(WebUiConnectionString);
            var connectSrv = new ServerConnection(connectSql);

            new Server(connectSrv).ConnectionContext.ExecuteNonQuery(scriptText);
        }

        public static void RunScript(string connectionString, string scriptPath)
        {
            var scriptText = File.ReadAllText(scriptPath);

            var connectSql = new SqlConnection(connectionString);
            var connectSrv = new ServerConnection(connectSql);

            new Server(connectSrv).ConnectionContext.ExecuteNonQuery(scriptText);
        }
    }
}