using Serilog;

namespace Api.Configuration.Logging
{
    internal static class LoggingBootstrapper
    {
        public static IHostBuilder ConfigureLogging(this IHostBuilder host)
        {
            return host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));
        }
    }
}
