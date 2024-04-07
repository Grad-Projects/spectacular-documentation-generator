using Api.Configuration;
using Api.Configuration.Logging;
using Microsoft.Extensions.Hosting;
using Serilog;

var logBuilder = new ConfigurationBuilder();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(logBuilder.Build())
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting Web Host");
    var builder = WebApplication.CreateBuilder(args);

    logBuilder.AddJsonFile("appsettings.json", true, true);
    builder.Services.ConfigureServices(builder.Configuration);

    builder.Host.ConfigureLogging();

    var app = builder.Build();
    app.ConfigureApp(app.Environment);
    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Host shut down unexpectedly");
    Log.CloseAndFlush();
}