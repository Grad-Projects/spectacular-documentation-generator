using SpectactularCLI.Commands;
using Microsoft.Extensions.Configuration;

namespace SpectactularCLI.Utilities
{
    public static class Global
    {
        public const string AUTH0_DOMAIN = "";
        public const string AUTH0_CLIENT_ID = "";
        public const string AUTH0_AUDIENCE = "";
        public const string API_DOMAIN = "";
        public static string AccessToken = "";
        public static string User = "";
        public static HttpClient Client = new();
        public static IConfigurationRoot config = new ConfigurationBuilder()
            .AddUserSecrets<App>()
            .Build();
        public static string githubClientID = config["github_client_id"];

        public static readonly List<Command> DefaultCommands = new List<Command>
        {
            new LoginCommand()
        };

        public static List<Command> Commands = DefaultCommands;
    }
}
