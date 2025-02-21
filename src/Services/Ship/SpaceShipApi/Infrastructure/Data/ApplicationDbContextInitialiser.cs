using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public static class InitialiserExtentensions
{
    public static async Task InitialiseDbAsync(this WebApplication app)
    {
           using var scope = app.Services.CreateScope();

          var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

           await initialiser.InitialiseAsync();
    }
}

public sealed class ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, IConfiguration configuration)
{
    public async Task InitialiseAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
            await RunSqlScriptAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occured while initialising database");
        }
    }

    private async Task RunSqlScriptAsync()
    {
        var connectionString = configuration.GetConnectionString("ConnectionString");
        var scriptName = configuration.GetValue<string>("SeedFile");
        if (string.IsNullOrEmpty(scriptName))
        {
            Console.WriteLine("⚠️ SQL seed name not set!");
            return;
        }
        var basePath = AppContext.BaseDirectory;
        var scriptPath = Path.Combine(basePath, scriptName);
        
        if (File.Exists(scriptPath))
        {
            var script = await File.ReadAllTextAsync(scriptPath);

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(script, connection);
            await command.ExecuteNonQueryAsync();

            Console.WriteLine("✅ SQL script executed successfully!");
        }
        else
        {
            Console.WriteLine("⚠️ SQL seed script not found!");
        }
    }
}