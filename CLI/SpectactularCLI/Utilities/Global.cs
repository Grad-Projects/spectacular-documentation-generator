using SpectactularCLI.Commands;
using Microsoft.Extensions.Configuration;

namespace SpectactularCLI.Utilities
{
    public static class Global
    {
        public const string API_DOMAIN = "http://spectacular-generator.eba-833qa9rw.eu-west-1.elasticbeanstalk.com";
        public static string AccessToken = "";
        public static int ApiVersion = 1;
        public static HttpClient Client = new();
        public static string githubClientID = "Iv1.be664de67499daba";

        public static readonly List<Command> DefaultCommands = new List<Command>
        {
            new LoginCommand()
        };

        public static List<Command> Commands = DefaultCommands;

        public static readonly List<string> StyleList = new List<string>
        {
            "Simple",
            "Serious Business",
            "Pastel Dreams",
            "Eye Searer",
            "I Love To Code"
        };

        public static string SelectedStyle = "";
    }
}
