using Application.Repositories;
using Infrastructure.Configuration;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var apiSettings = configuration.GetSection("ApiSettings").Get<ApiSettings>();
        if (apiSettings == null)
        {
            throw new Exception();
        }

        services.AddHttpClient<IUserRepository, UserRepository>((serviceProvider, client) =>
        {
            // Get the UserApiBaseUri from the configuration
            client.BaseAddress = new Uri(apiSettings.UserApiBaseUri);
        });
        
        return services;
    }
}
