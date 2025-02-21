using Application.Interfaces;
using Application.Services;
using Infrastructure.Configuration;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var apiSettings = configuration.GetSection("ApiSettings").Get<ApiSettings>() ?? throw new Exception();
        services.AddHttpClient<ISpaceShipRepository, SpaceShipRepository>((serviceProvider, client) =>
        {
            // Get the UserApiBaseUri from the configuration
            client.BaseAddress = new Uri(apiSettings.UserApiBaseUri);
       });
        
        return services;
    }
}
