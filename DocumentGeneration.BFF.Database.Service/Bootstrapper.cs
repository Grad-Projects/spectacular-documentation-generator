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
            services.AddScoped<DatabaseService>();
            services.AddScoped<getStyleFromDB>(s => s.GetRequiredService<DatabaseService>().getStyleFromDB);
            services.AddScoped<postDocumentToDB>(s => s.GetRequiredService<DatabaseService>().postDocumentToDB);
            services.AddScoped<checkUserInDB>(s => s.GetRequiredService<DatabaseService>().checkUserInDB);
            services.AddScoped<addUserToDb>(s => s.GetRequiredService<DatabaseService>().addUserToDb);
            services.AddScoped<checkStyle>(s => s.GetRequiredService<DatabaseService>().checkStyle);
            return services;
        }

    }
}
