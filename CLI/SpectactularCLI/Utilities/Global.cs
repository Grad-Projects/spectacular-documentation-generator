using SpectactularCLI.Commands;

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

        public static readonly List<Command> DefaultCommands = new List<Command>
        {

        };

        public static List<Command> Commands = DefaultCommands;
    }
}
