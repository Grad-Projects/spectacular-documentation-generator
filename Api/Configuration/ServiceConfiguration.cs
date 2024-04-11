using Api.Configuration.Swagger;
using DocumentGeneration.BFF.Core;
using DocumentGeneration.BFF.DocumentationGen.Service;
using DocumentGeneration.BFF.HtmlConverter.Service;
using DocumentGeneration.BFF.Database.Service;

namespace Api.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            services.AddConfigurationServices(configuration)
            .AddDocGenService(configuration)
            .AddHtmlConverterService(configuration)
            .AddDatabaseService(configuration)
            .AddCoreServices(configuration);

            return services;
        }

        private static IServiceCollection AddConfigurationServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSwaggerGeneration();
        }
    }
}
