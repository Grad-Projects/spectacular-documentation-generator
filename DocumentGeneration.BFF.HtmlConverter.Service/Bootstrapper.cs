using DocumentGeneration.BFF.Core.Operations;
using DocumentGeneration.BFF.HtmlConverter.Service.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentGeneration.BFF.HtmlConverter.Service
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddHtmlConverterService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<HtmlConverterService>();
            services.AddScoped<ConvertToHtml>(s => s.GetRequiredService<HtmlConverterService>().ToHtml);
            return services;
        }

    }
}
