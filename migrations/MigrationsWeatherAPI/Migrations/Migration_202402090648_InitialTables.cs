using FluentMigrator;

namespace MigrationsWeatherAPI;

[Migration(202402090648)]
public class Migration_202402090648_InitialTables : Migration
{
    public override void Up()
    {
        Create.Table("WeatherForecast")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Date").AsDate().NotNullable()
            .WithColumn("Summary").AsString(100).NotNullable()
            .WithColumn("TemperatureC").AsInt32().NotNullable()
            .WithColumn("TemperatureF").AsInt32().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("WeatherForecast");
    }
}
