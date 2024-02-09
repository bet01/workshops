using FluentMigrator.Runner;

namespace MigrationsWeatherAPI.Migrations;
public static class MigrationManager
{
    public static IHost MigrateDatabase(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var databaseService = scope.ServiceProvider.GetRequiredService<Database>();
            var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

            databaseService.CreateDatabase("Weather");

            migrationService.ListMigrations();
            migrationService.MigrateUp();
        }

        return host;
    }
}
