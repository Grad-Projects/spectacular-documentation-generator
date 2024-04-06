using Api.Configuration.Swagger;

namespace Api.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddConfigurationServices(configuration);

            return services;
        }

        private static IServiceCollection AddConfigurationServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSwaggerGeneration();
        }
    }
}
