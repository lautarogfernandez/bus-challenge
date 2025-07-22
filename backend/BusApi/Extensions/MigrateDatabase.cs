using BusApi.Data;

namespace BusApi.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            using (var appContext = scope.ServiceProvider.GetRequiredService<BusContext>())
                try
                {
                    appContext.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    var looger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    looger.LogError(ex, "An error ocurred configuring the DB.");
                }

            return host;
        }
    }
}