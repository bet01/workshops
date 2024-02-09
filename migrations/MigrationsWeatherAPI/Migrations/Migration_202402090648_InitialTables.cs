using FluentMigrator;

namespace MigrationsWeatherAPI;

[Migration(202402090648, TransactionBehavior.None)]
public class Migration_202402090648_InitialTables : Migration
{
    public override void Up()
    {
        Execute.Sql(@"
            CREATE TABLE WeatherForecast (
                Id INT IDENTITY(1,1) PRIMARY KEY NONCLUSTERED,
                Date DATE NOT NULL,
                Summary NVARCHAR(100) NOT NULL,
                TemperatureC INT NOT NULL,
                TemperatureF INT NOT NULL
            ) WITH (MEMORY_OPTIMIZED=ON);");

        // FluentMigrator equivalent:
        // Create.Table("WeatherForecast")
        //     .WithColumn("Id").AsInt32().PrimaryKey().Identity()
        //     .WithColumn("Date").AsDate().NotNullable()
        //     .WithColumn("Summary").AsString(100).NotNullable()
        //     .WithColumn("TemperatureC").AsInt32().NotNullable()
        //     .WithColumn("TemperatureF").AsInt32().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("WeatherForecast");
    }
}
