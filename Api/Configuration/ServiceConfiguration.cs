using Api.Configuration.Swagger;
using DocumentGeneration.BFF.Core;
using DocumentGeneration.BFF.DocumentationGen.Service;

namespace Api.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddConfigurationServices(configuration)
            .AddDocGenService(configuration)
            .AddCoreServices(configuration);

            return services;
        }

        private static IServiceCollection AddConfigurationServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSwaggerGeneration();
        }
    }
}
