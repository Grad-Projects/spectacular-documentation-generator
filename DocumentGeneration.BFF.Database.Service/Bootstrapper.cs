using DocumentGeneration.BFF.Database.Service.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DocumentGeneration.BFF.Database.Service.Models;
using DocumentGeneration.BFF.Core.Operations;

namespace DocumentGeneration.BFF.Database.Service
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddDatabaseService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<DatabaseOptions>()
                .BindConfiguration($"{DatabaseOptions.Section}");
            services.AddScoped<DatabaseService>();
            services.AddScoped<getStyleFromDB>(s => s.GetRequiredService<DatabaseService>().getStyleFromDB);
            return services;
        }

    }
}
