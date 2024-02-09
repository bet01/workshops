using Dapper;
using MigrationsWeatherAPI.Infrastructure;

namespace MigrationsWeatherAPI.Migrations;
public class Database
{
    private readonly DapperContext _context;

    public Database(DapperContext context)
    {
        _context = context;
    }

    public void CreateDatabase(string dbName)
    {
        var query = "SELECT * FROM sys.databases WHERE name = @name";
        var parameters = new DynamicParameters();
        parameters.Add("name", dbName);

        using (var connection = _context.CreateMasterConnection())
        {
            var records = connection.Query(query, parameters);
            if (!records.Any())
            {
                connection.Execute($"CREATE DATABASE {dbName}");
                connection.Execute(
                    $@"ALTER DATABASE {dbName} 
                    SET MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = ON;");
                connection.Execute(
                    $@"ALTER DATABASE {dbName} 
                    ADD FILEGROUP {dbName}_mod CONTAINS MEMORY_OPTIMIZED_DATA;");
                connection.Execute(
                    $@"ALTER DATABASE {dbName} ADD FILE (
                    name='{dbName}_mod1', filename='/var/opt/mssql/data/{dbName}_mod1')
                    TO FILEGROUP {dbName}_mod;");
            }
        }
    }
}
