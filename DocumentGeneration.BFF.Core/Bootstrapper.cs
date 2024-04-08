using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentGeneration.BFF.Core.Interfaces;
using DocumentGeneration.BFF.Core.Usecases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DocumentGeneration.BFF.Core
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGenerateDocumentationUsecase, GenerateDocumentationUsecase>();
            services.AddScoped<ITestCase, loggingTestUsecase>();
            return services;
        }
    }
}
