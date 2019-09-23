using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace WEB_UI
{
    public static class Database
    {
        public static readonly string ConnectionString;

        public static string WebUIscriptFileName { get; }

        static Database()
        {
            ConnectionString = @"Data Source=DEN090312\SQLEXPRESS;Initial Catalog=webusersdb;Integrated Security=True";
            WebUIscriptFileName = "~/Scripts/webusersdb.sql";
        }

        public static void RunScript(HttpServerUtilityBase server, string path)
        {
            var scriptPath = server.MapPath(path);
            var scriptText = File.ReadAllText(scriptPath);

            var connectSql = new SqlConnection(ConnectionString);
            var connectSrv = new ServerConnection(connectSql);

            new Server(connectSrv).ConnectionContext.ExecuteNonQuery(scriptText);
        }

        public static void RunScript(string path)
        {
            var scriptText = File.ReadAllText(path);

            var connectSql = new SqlConnection(ConnectionString);
            var connectSrv = new ServerConnection(connectSql);

            new Server(connectSrv).ConnectionContext.ExecuteNonQuery(scriptText);
        }
    }
}