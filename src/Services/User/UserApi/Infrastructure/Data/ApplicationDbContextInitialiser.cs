using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
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

public sealed class ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
{
    public async Task InitialiseAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occured while initialising database");
        }
    }
}