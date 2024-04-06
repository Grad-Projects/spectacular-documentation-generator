using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DocumentGeneration.BFF.DocumentationGen.Service.Service;
using DocumentGeneration.BFF.Core.Operations;

namespace DocumentGeneration.BFF.DocumentationGen.Service
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddStudentService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<CodeAnalyzerService>();
            services.AddScoped<AnalyzeCode>(s => s.GetRequiredService<CodeAnalyzerService>().Analyze);
            return services;
        }
    }
}
