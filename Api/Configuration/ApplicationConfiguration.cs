using Api.Endpoints;
using Serilog;

namespace Api.Configuration
{
    public static class ApplicationConfiguration
    {
        public static WebApplication ConfigureApp(this WebApplication app, IWebHostEnvironment env)
        {
            app.UseApiEndpoints();
            app.UseSerilogRequestLogging();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(
                options =>
                {
                    var versions = app.DescribeApiVersions();
                    foreach (var version in versions)
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{version.GroupName}/swagger.json",
                            version.GroupName.ToUpperInvariant());
                    }
                });
            }
            app.UseRouting();
            return app;
        }
    }
}
