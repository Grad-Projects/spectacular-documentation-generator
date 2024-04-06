using Asp.Versioning;

namespace Api.Configuration.Swagger
{
    internal static class SwaggerBootstrapper
    {
        public static IServiceCollection AddSwaggerGeneration(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer()
                .AddSwaggerGen(c => {

                })
                .AddApiVersioning(config =>
                {
                    config.DefaultApiVersion = new ApiVersion(1, 0);
                    config.AssumeDefaultVersionWhenUnspecified = true;
                    config.ApiVersionReader = ApiVersionReader.Combine(
                        new UrlSegmentApiVersionReader(),
                        new QueryStringApiVersionReader("api-version"));
                })
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.FormatGroupName = (group, version) => $"{group} - {version}";
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.AddApiVersionParametersWhenVersionNeutral = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.SubstituteApiVersionInUrl = true;
                });

            return services;
        }
    }
}
