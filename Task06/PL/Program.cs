namespace Pl
{
    public class Program
    {
        private static void Main(string[] args)
        {
            //var connectionString = @"Data Source=DEN090312\SQLEXPRESS;Initial Catalog=webusersdb;Integrated Security=True";
            //var scriptPath = @"C:\Users\Bolotin\source\repos\NewRepo2\Task06\Scripts\webusersdb.sql";

            //WEB_UI.Database.RunScript(connectionString, scriptPath);

            new OutputPl().Run();
        }
    }
}