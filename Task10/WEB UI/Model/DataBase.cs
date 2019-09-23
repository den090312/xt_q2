using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace WEB_UI
{
    public static class Database
    {
        public static HttpServerUtilityBase MyServer { private get; set; }

        public static readonly string ConnectionString;

        public static string ScriptFileName { get; set; }

        static Database()
        {
            ConnectionString = @"Data Source=DEN090312\SQLEXPRESS;Initial Catalog=webusersdb;Integrated Security=True";
            ScriptFileName = "~/Scripts/webusersdb.sql";
        }

        public static void RunScript()
        {
            var sqriptPath = MyServer.MapPath(ScriptFileName);
            var scriptText = File.ReadAllText(sqriptPath);

            var connectSql = new SqlConnection(ConnectionString);
            var connectSrv = new ServerConnection(connectSql);

            var server = new Server(connectSrv);
            server.ConnectionContext.ExecuteNonQuery(scriptText);
        }
    }
}